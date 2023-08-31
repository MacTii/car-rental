using Domain.Entities;
using Application.Mapper.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.Interfaces.Helpers;
using Domain.Models;
using Azure.Core;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        #region Injection

        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IUserRepository _userRepository;

        private readonly IAuthenticationHelper _authenticationHelper;

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, IUserCredentialsRepository userCredentialsRepository,
            IUserRepository userRepository, IAuthenticationHelper authenticationHelper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userCredentialsRepository = userCredentialsRepository;
            _userRepository = userRepository;
            _authenticationHelper = authenticationHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Injection

        public string GetMyUsername()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                Claim? claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                result = claim != null ? claim.Value : string.Empty;
            }
            return result;
        }

        public string Register(RegisterDTO request)
        {
            var existingUser = _userCredentialsRepository.GetByUsername(request.Username);
            if (existingUser != null)
                throw new InvalidOperationException("This username is taken!");

            if (request.FirstPassword != request.SecondPassword)
                throw new ArgumentException("The passwords provided are not identical!");

            _authenticationHelper.CreatePasswordHash(request.FirstPassword, out byte[] passwordHash, out byte[] passwordSalt);

            // mapping RegisterDTO -> UserCredentials
            var userCredentials = _mapper.Map<UserCredentials>(request);
            userCredentials.PasswordHash = passwordHash;
            userCredentials.PasswordSalt = passwordSalt;

            string token = _authenticationHelper.GetToken(userCredentials);

            // Create UserCredentials with new RefreshToken
            _userCredentialsRepository.Insert(userCredentials);
            _userCredentialsRepository.Save();

            // mapping RegisterDTO -> User
            var user = _mapper.Map<User>(request);
            user.UserCredentials = userCredentials;

            // Create new User
            _userRepository.Insert(user);
            _userRepository.Save();

            return token;
        }

        public string Login(LoginDTO request)
        {
            var userCredentials = _userCredentialsRepository.GetByUsername(request.Username);
            if (userCredentials == null)
                throw new InvalidOperationException("Wrong username!");

            if (!_authenticationHelper.VerifyPasswordHash(request.Password, userCredentials.PasswordHash, userCredentials.PasswordSalt))
                throw new UnauthorizedAccessException("Wrong password!");

            string token = _authenticationHelper.GetToken(userCredentials);

            // Update UserCredentials with RefreshToken
            _userCredentialsRepository.Update(userCredentials.ID, userCredentials);
            _userCredentialsRepository.Save();

            return token;
        }

        public string RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            var username = GetMyUsername();
            var userCredentials = _userCredentialsRepository.GetByUsername(username);
            if (userCredentials == null)
                throw new InvalidOperationException("User credentials not found!"); // KeyNotFoundException, NotFoundException

            if (!userCredentials.RefreshToken.Equals(refreshToken))
            {
                throw new UnauthorizedAccessException("Invalid Refresh Token!"); // Unauthorized, UnauthorizedException();
            }
            else if (userCredentials.TokenExpires < DateTime.Now)
            {
                throw new SecurityTokenExpiredException("Token expired!"); // Unauthorized, TokenExpiredException();
            }

            string token = _authenticationHelper.GetToken(userCredentials);

            // Update UserCredentials with new RefreshToken
            _userCredentialsRepository.Update(userCredentials.ID, userCredentials);
            _userCredentialsRepository.Save();

            return token;
        }

        public AuthenticationDataDTO GetPasswordCredentials()
        {
            // Returns model PasswordCredential => (PasswordHash, PasswordSalt)
            var passwordCredential = _authenticationHelper.GenerateRandomPasswordCredential();

            // Returns model RefreshToken => (RefreshToken, TokenCreated, TokenExpires)
            var refreshToken = _authenticationHelper.GenerateRefreshToken();

            var authenticationDataDTO = _mapper.Map<AuthenticationDataDTO>((passwordCredential, refreshToken));

            return authenticationDataDTO;
        }
    }
}

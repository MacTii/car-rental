using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.Mapper.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        #region Injection

        private readonly IMapper _mapper;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IUserRepository _userRepository;
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

        public UserCredentials Register(RegisterDTO request)
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

            // mapping RegisterDTO -> User
            var user = _mapper.Map<User>(request);
            user.UserCredentials = userCredentials;

            _userRepository.Insert(user);
            _userRepository.Save();

            return userCredentials;
        }

        public string Login(LoginDTO request)
        {
            var user = _userCredentialsRepository.GetByUsername(request.Username);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            if (!_authenticationHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Wrong password!");

            string token = _authenticationHelper.CreateToken(user);

            var refreshToken = _authenticationHelper.GenerateRefreshToken();
            _authenticationHelper.SetRefreshToken(refreshToken, user);

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

            string token = _authenticationHelper.CreateToken(userCredentials);

            var newRefreshToken = _authenticationHelper.GenerateRefreshToken();
            _authenticationHelper.SetRefreshToken(newRefreshToken, userCredentials);

            return token;
        }
    }
}

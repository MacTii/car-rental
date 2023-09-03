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
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IPasswordHelper _passwordHelper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserContextHelper _userContextHelper;

        public AuthService(IUserCredentialsRepository userCredentialsRepository, IPasswordHelper passwordHelper,
            IHttpContextAccessor httpContextAccessor, ITokenHelper tokenHelper, IUserContextHelper userContextHelper)
        {
            _userCredentialsRepository = userCredentialsRepository;
            _httpContextAccessor = httpContextAccessor;
            _passwordHelper = passwordHelper;
            _tokenHelper = tokenHelper;
            _userContextHelper = userContextHelper;
        }

        #endregion Injection

        public string Login(LoginDTO request)
        {
            var userCredentials = _userCredentialsRepository.GetByUsername(request.Username);
            if (userCredentials == null)
                throw new InvalidOperationException("Wrong username!");

            if (!_passwordHelper.VerifyPasswordHash(request.Password, userCredentials.PasswordHash, userCredentials.PasswordSalt))
                throw new UnauthorizedAccessException("Wrong password!");

            string token = _tokenHelper.GetToken(userCredentials);

            // Update UserCredentials with RefreshToken
            _userCredentialsRepository.Update(userCredentials.ID, userCredentials);
            _userCredentialsRepository.Save();

            return token;
        }

        public string RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            var username = _userContextHelper.GetMyUsername();
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

            string token = _tokenHelper.GetToken(userCredentials);

            // Update UserCredentials with new RefreshToken
            _userCredentialsRepository.Update(userCredentials.ID, userCredentials);
            _userCredentialsRepository.Save();

            return token;
        }
    }
}

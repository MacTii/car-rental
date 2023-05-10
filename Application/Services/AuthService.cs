﻿using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.Mapper.DTOs;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        #region Injection

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IUserRepository _userRepository;
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IMapper mapper, IUserCredentialsRepository userCredentialsRepository,
            IUserRepository userRepository, IAuthenticationHelper authenticationHelper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userCredentialsRepository = userCredentialsRepository;
            _userRepository = userRepository;
            _authenticationHelper = authenticationHelper;
        }

        #endregion Injection

        public string GetMyName()
        {
            /*var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;*/
            return string.Empty;
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

            // var refreshToken = GenerateRefreshToken();
            // SetRefreshToken(refreshToken);

            return token;
        }

        /*public async Task<string> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);

            return token;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, UserCredentials user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }*/
    }
}

using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class RegistrationService : IRegistrationService
    {

        #region Injection

        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        private readonly IPasswordHelper _passwordHelper;
        private readonly ITokenHelper _tokenHelper;

        public RegistrationService(IMapper mapper, IUserCredentialsRepository userCredentialsRepository, IPasswordHelper passwordHelper,
            IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ITokenHelper tokenHelper)
        {
            _mapper = mapper;
            _userCredentialsRepository = userCredentialsRepository;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _tokenHelper = tokenHelper;
        }

        #endregion Injection

        public string Register(RegisterDTO request)
        {
            var existingUserByUsername = _userCredentialsRepository.GetByUsername(request.Username);
            if (existingUserByUsername != null)
                throw new InvalidOperationException("This username is taken!");

            var existingUserByEmail = _userRepository.GetByEmail(request.Email);
            if (existingUserByEmail != null)
                throw new InvalidOperationException("This email address is already in use!");

            if (request.FirstPassword != request.SecondPassword)
                throw new ArgumentException("The passwords provided are not identical!");

            _passwordHelper.CreatePasswordHash(request.FirstPassword, out byte[] passwordHash, out byte[] passwordSalt);

            // mapping RegisterDTO -> UserCredentials
            var userCredentials = _mapper.Map<UserCredentials>(request);
            userCredentials.PasswordHash = passwordHash;
            userCredentials.PasswordSalt = passwordSalt;

            string token = _tokenHelper.GetToken(userCredentials);

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
    }
}

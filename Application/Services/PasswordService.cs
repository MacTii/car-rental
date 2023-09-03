using Application.Helpers;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Mapper.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        #region Injection

        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public PasswordService(IPasswordHelper passwordHelper, IMapper mapper, IUserRepository userRepository)
        {
            _passwordHelper = passwordHelper;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #endregion Injection

        public PasswordCredentialDTO GetPasswordCredentials()
        {
            // Returns model PasswordCredential => (PasswordHash, PasswordSalt)
            var passwordCredential = _passwordHelper.GenerateRandomPasswordCredential();

            var authenticationDataDTO = _mapper.Map<PasswordCredentialDTO>(passwordCredential);

            return authenticationDataDTO;
        }

        public void ResetPassword(int userID)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _userRepository.GetByID(userID);
            if (user == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            // Returns model PasswordCredential => (PasswordHash, PasswordSalt)
            var passwordCredential = _passwordHelper.GenerateRandomPasswordCredential();

            user.UserCredentials.PasswordHash = passwordCredential.PasswordHash;
            user.UserCredentials.PasswordSalt = passwordCredential.PasswordSalt;
            user.UserCredentials.RefreshToken = null;
            user.UserCredentials.TokenCreated = null;
            user.UserCredentials.TokenExpires = null;

            _userRepository.Update(userID, user);
            _userRepository.Save();
        }
    }
}

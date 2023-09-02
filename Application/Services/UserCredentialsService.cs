using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Mapper.DTOs;
using Domain.Entities;
using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Application.Validators;
using FluentValidation;

namespace Application.Services
{
    public class UserCredentialsService : IUserCredentialsService
    {
        #region Injection

        private readonly ILogger<UserCredentialsService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        private readonly UserCredentialValidator _validator;

        public UserCredentialsService(ILogger<UserCredentialsService> logger, IMapper mapper, IUserCredentialsRepository userCredentialsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userCredentialsRepository = userCredentialsRepository;
            _validator = new UserCredentialValidator();
        }

        #endregion Injection

        public IEnumerable<UserCredentialsDTO> GetUserCredentials()
        {
            var userCredentials = _userCredentialsRepository.GetAll();
            return _mapper.Map<IEnumerable<UserCredentialsDTO>>(userCredentials);
        }

        public UserCredentialsDTO GetUserCredential(int userCredentialsID)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. User credentials ID must be greater " +
                    $"than or equal to 1.");

            var userCredentials = _userCredentialsRepository.GetByID(userCredentialsID);
            if(userCredentials == null)
                throw new InvalidOperationException($"User credentials with ID: {userCredentialsID} not found.");

            return _mapper.Map<UserCredentialsDTO>(userCredentials);
        }

        public void AddUserCredential(UserCredentialsDTO userCredentialsDTO)
        {
            // Check if the input data is valid
            var validationResult = _validator.Validate(userCredentialsDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _userCredentialsRepository.Insert(_mapper.Map<UserCredentials>(userCredentialsDTO));
            _userCredentialsRepository.Save();
        }

        public void UpdateUserCredential(int userCredentialsID, UserCredentialsDTO userCredentialsDTO)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. User credentials ID must be greater " +
                    $"than or equal to 1.");

            var userCredentials = _userCredentialsRepository.GetByID(userCredentialsID);
            if (userCredentials == null)
                throw new InvalidOperationException($"User credentials with ID: {userCredentialsID} not found.");

            // Check if the input data is valid
            var validationResult = _validator.Validate(userCredentialsDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _userCredentialsRepository.Update(userCredentialsID, _mapper.Map<UserCredentials>(userCredentialsDTO));
            _userCredentialsRepository.Save();
        }

        public void DeleteUserCredential(int userCredentialsID)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. User credentials ID must be greater " +
                    $"than or equal to 1.");

            var userCredentials = _userCredentialsRepository.GetByID(userCredentialsID);
            if (userCredentials == null)
                throw new InvalidOperationException($"User credentials with ID: {userCredentialsID} not found.");

            _userCredentialsRepository.Delete(userCredentialsID);
            _userCredentialsRepository.Save();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        #region Injection

        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #endregion Injection

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserByID(int userID)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _userRepository.GetByID(userID);
            if (user == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
                throw new InvalidOperationException($"User with username: {username} not found.");

            return _mapper.Map<UserDTO>(user);
        }

        public void AddUser(UserDTO userDTO)
        {
            var existingUserByUsernam = _userRepository.GetByUsername(userDTO.UserCredentials.Username);
            if (existingUserByUsernam != null)
                throw new InvalidOperationException($"This username: {userDTO.UserCredentials.Username} is taken!");

            var existingUserByEmail = _userRepository.GetByEmail(userDTO.Email);
            if (existingUserByEmail != null)
                throw new InvalidOperationException($"This email address: {userDTO.Email} is already in use!");

            _userRepository.Insert(_mapper.Map<User>(userDTO));
            _userRepository.Save();
        }

        public void UpdateUser(int userID, UserDTO userDTO)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _userRepository.GetByID(userID);
            if (user == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            _userRepository.Update(userID, _mapper.Map<User>(userDTO));
            _userRepository.Save();
        }

        public void DeleteUser(int userID)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _userRepository.GetByID(userID);
            if (user == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            _userRepository.Delete(userID);
            _userRepository.Save();
        }
    }
}

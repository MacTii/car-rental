using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using AutoMapper;
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
            var user = _userRepository.GetByID(userID);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return _mapper.Map<UserDTO>(user);
        }

        public void AddUser(UserDTO userDTO)
        {
            _userRepository.Insert(_mapper.Map<User>(userDTO));
            _userRepository.Save();
        }

        public void UpdateUser(int userID, UserDTO userDTO)
        {
            _userRepository.Update(userID, _mapper.Map<User>(userDTO));
            _userRepository.Save();
        }

        public void DeleteUser(int userID)
        {
            _userRepository.Delete(userID);
            _userRepository.Save();
        }
    }
}

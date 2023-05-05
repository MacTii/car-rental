using Application.Interfaces;
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
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        #endregion Injection

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetAll();
            return users;
        }

        public User GetUser(int userID)
        {
            var user = _userRepository.GetByID(userID);
            return user;
        }

        public void AddUser(User user)
        {
            _userRepository.Insert(user);
            _userRepository.Save();
        }

        public void UpdateUser(int userID, User user)
        {
            _userRepository.Update(userID, user);
            _userRepository.Save();
        }

        public void DeleteUser(int userID)
        {
            _userRepository.Delete(userID);
            _userRepository.Save();
        }
    }
}

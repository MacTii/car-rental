﻿using Application.Mapper.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        public IEnumerable<UserDTO> GetUsers();
        public UserDTO GetUserByID(int userID);
        public UserDTO GetUserByUsername(string username);
        public void AddUser(UserDTO userDTO);
        public void UpdateUser(int userID, UserDTO userDTO);
        public void DeleteUser(int userID);
    }
}

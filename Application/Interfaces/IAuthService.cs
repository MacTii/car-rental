using Application.Mapper.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        public UserCredentials Register(RegisterDTO request);
        public string Login(LoginDTO request);
    }
}

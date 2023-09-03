using Application.Mapper.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRegistrationService
    {
        public string Register(RegisterDTO request);
    }
}

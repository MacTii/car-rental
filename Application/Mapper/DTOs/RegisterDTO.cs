using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; set; } = null!;
        public string FirstPassword { get; set; } = null!;
        public string SecondPassword { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string IdentificationNumber { get; set; } = null!;
        public string DrivingLicenseNumber { get; set; } = null!;
    }
}

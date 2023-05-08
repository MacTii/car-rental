using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public int UserCredentialsID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string DrivingLicenseNumber { get; set; } = null!;
    }
}

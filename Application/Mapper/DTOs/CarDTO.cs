using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class CarDTO
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
    }
}

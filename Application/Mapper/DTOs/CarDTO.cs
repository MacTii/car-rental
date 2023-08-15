using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class CarDTO
    {
        public int ID { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public int Year { get; set; }
        public string Color { get; set; } = null!;
        public string Engine { get; set; } = null!;
        public int Speed { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; } = null!; // base64 string
    }
}

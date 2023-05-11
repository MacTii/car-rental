using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car
    {
        public int ID { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public int Year { get; set; }
        public ColorEnum Color { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}

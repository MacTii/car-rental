using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rental
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public int UserID { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Comment { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public Car Car { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}

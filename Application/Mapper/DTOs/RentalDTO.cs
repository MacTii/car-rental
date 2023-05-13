using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.DTOs
{
    public class RentalDTO
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public int UserID { get; set; }
        public string RentDate { get; set; } = null!;
        public string? ReturnDate { get; set; }
        public string? Comments { get; set; }
    }
}

using Application.Mapper.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRentalService
    {
        public IEnumerable<RentalDTO> GetRentals();
        public RentalDTO GetRentalByID(int rentalID);
        public IEnumerable<RentalDTO> GetRentalsByUsername(string username);
        public void AddRental(RentalDTO rentalDTO);
        public void UpdateRental(int rentalID, RentalDTO rentalDTO);
        public void DeleteRental(int rentalID);
    }
}

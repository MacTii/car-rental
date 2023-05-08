using Application.Interfaces;
using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RentalService : IRentalService
    {
        #region Injection

        private readonly ILogger<RentalService> _logger;
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public RentalService(ILogger<RentalService> logger, IMapper mapper, IRentalRepository rentalRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rentalRepository = rentalRepository;
        }

        #endregion Injection

        public IEnumerable<RentalDTO> GetRentals()
        {
            var rentals = _rentalRepository.GetAll();
            return _mapper.Map<IEnumerable<RentalDTO>>(rentals);
        }

        public RentalDTO GetRental(int rentalID)
        {
            var rental = _rentalRepository.GetByID(rentalID);
            return _mapper.Map<RentalDTO>(rental);
        }

        public void AddRental(RentalDTO rentalDTO)
        {
            _rentalRepository.Insert(_mapper.Map<Rental>(rentalDTO));
            _rentalRepository.Save();
        }

        public void UpdateRental(int rentalID, RentalDTO rentalDTO)
        {
            _rentalRepository.Update(rentalID, _mapper.Map<Rental>(rentalDTO));
            _rentalRepository.Save();
        }

        public void DeleteRental(int rentalID)
        {
            _rentalRepository.Delete(rentalID);
            _rentalRepository.Save();
        }
    }
}

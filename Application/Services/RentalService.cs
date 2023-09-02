using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
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

        public RentalDTO GetRentalByID(int rentalID)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var rental = _rentalRepository.GetByID(rentalID);
            if(rental == null)
                throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");

            return _mapper.Map<RentalDTO>(rental);
        }

        public IEnumerable<RentalDTO> GetRentalsByUsername(string username)
        {
            var rentals = _rentalRepository.GetByUsername(username);
            return _mapper.Map<IEnumerable<RentalDTO>>(rentals);
        }

        public void AddRental(RentalDTO rentalDTO)
        {
            _rentalRepository.Insert(_mapper.Map<Rental>(rentalDTO));
            _rentalRepository.Save();
        }

        public void UpdateRental(int rentalID, RentalDTO rentalDTO)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var rental = _rentalRepository.GetByID(rentalID);
            if (rental == null)
                throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");

            _rentalRepository.Update(rentalID, _mapper.Map<Rental>(rentalDTO));
            _rentalRepository.Save();
        }

        public void DeleteRental(int rentalID)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var rental = _rentalRepository.GetByID(rentalID);
            if (rental == null)
                throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");

            _rentalRepository.Delete(rentalID);
            _rentalRepository.Save();
        }
    }
}

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<Rental> GetAll()
        {
            var rentals = _context.Rentals.ToList();
            return rentals;
        }

        public Rental GetByID(int rentalID)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var rental = _context.Rentals.Find(rentalID);
            return rental ?? throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");
        }

        public void Insert(Rental rental)
        {
            _context.Rentals.Add(rental);
        }

        public void Update(int rentalID, Rental rental)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var existingRental = _context.Rentals.Find(rentalID);
            if (existingRental == null)
                throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");

            existingRental.RentDate = rental.RentDate;
            existingRental.ReturnDate = rental.ReturnDate;
            existingRental.Comments = existingRental.Comments;

            _context.Entry(existingRental).State = EntityState.Modified;
        }

        public void Delete(int rentalID)
        {
            if (rentalID < 1)
                throw new ArgumentException($"Invalid rental ID: {rentalID}. Rental ID must be greater than or equal to 1.");

            var rental = _context.Rentals.Find(rentalID);
            if (rental == null)
                throw new InvalidOperationException($"Rental with ID: {rentalID} not found.");

            _context.Rentals.Remove(rental);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

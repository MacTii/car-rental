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
    public class CarRepository : ICarRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<Car> GetAll()
        {
            var cars = _context.Cars.ToList();
            return cars;
        }

        public Car GetByID(int carID)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _context.Cars.Find(carID);
            return car ?? throw new InvalidOperationException($"Car with ID: {carID} not found.");
        }

        public void Insert(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Update(int carID, Car car)
        {
            if(carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var existingCar = _context.Cars.Find(carID);
            if (existingCar == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.RegistrationNumber = car.RegistrationNumber;

            _context.Entry(existingCar).State = EntityState.Modified;
        }

        public void Delete(int carID)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _context.Cars.Find(carID);
            if (car == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            _context.Cars.Remove(car);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

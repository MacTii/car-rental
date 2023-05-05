using Application.Interfaces;
using Domain.Entities;
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
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = _context.Cars.ToList();
            return cars;
        }

        public Car GetByID(int carID)
        {
            var car = _context.Cars.Find(carID);
            return car ?? throw new ArgumentException("Invalid car ID");
        }

        public void Insert(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Update(int carID, Car car)
        {
            var existingCar = _context.Cars.Find(carID);
            if (existingCar == null)
            {
                throw new ArgumentException($"Car with ID {carID} not found.");
            }

            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.RegistrationNumber = car.RegistrationNumber;

            _context.Entry(existingCar).State = EntityState.Modified;
        }

        public void Delete(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

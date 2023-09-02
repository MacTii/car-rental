using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Car? GetByID(int carID)
        {
            var car = _context.Cars.Find(carID);
            return car;
        }

        public void Insert(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Update(int carID, Car car)
        {
            var existingCar = _context.Cars.Single(x => x.ID == carID);
            CopyProperties(car, existingCar);

            _context.Entry(existingCar).State = EntityState.Modified;
        }

        public void Delete(int carID)
        {
            var car = _context.Cars.Single(x=> x.ID == carID);
            _context.Cars.Remove(car);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private static void CopyProperties(object source, object destination)
        {
            Type type = source.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(destination, value);
                }
            }
        }
    }
}

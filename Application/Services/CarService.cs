using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CarService : ICarService
    {
        #region Injection

        private readonly ILogger<CarService> _logger;
        private readonly ICarRepository _carRepository;

        public CarService(ILogger<CarService> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        #endregion Injection

        public IEnumerable<Car> GetCars()
        {
            var cars = _carRepository.GetAll();
            return cars;
        }

        public Car GetCar(int carID)
        {
            var car = _carRepository.GetByID(carID);
            return car;
        }

        public void AddCar(Car car)
        {
            _carRepository.Insert(car);
            _carRepository.Save();
        }

        public void UpdateCar(int carID, Car car)
        {
            _carRepository.Update(carID, car);
            _carRepository.Save();
        }

        public void DeleteCar(int carID)
        {
            _carRepository.Delete(carID);
            _carRepository.Save();
        }
    }
}

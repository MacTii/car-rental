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
    public class CarService : ICarService
    {
        #region Injection

        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _carRepository = carRepository;
        }

        #endregion Injection

        public IEnumerable<CarDTO> GetCars()
        {
            var cars = _carRepository.GetAll();
            return _mapper.Map<IEnumerable<CarDTO>>(cars);
        }

        public CarDTO GetCar(int carID)
        {
            var car = _carRepository.GetByID(carID);
            return _mapper.Map<CarDTO>(car);
        }

        public void AddCar(CarDTO carDTO)
        {
            _carRepository.Insert(_mapper.Map<Car>(carDTO));
            _carRepository.Save();
        }

        public void UpdateCar(int carID, CarDTO carDTO)
        {
            _carRepository.Update(carID, _mapper.Map<Car>(carDTO));
            _carRepository.Save();
        }

        public void DeleteCar(int carID)
        {
            _carRepository.Delete(carID);
            _carRepository.Save();
        }
    }
}

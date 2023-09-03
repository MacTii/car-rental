using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Services
{
    public class CarService : ICarService
    {
        #region Injection

        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly CarValidator _validator;

        public CarService(ILogger<CarService> logger, IMapper mapper, ICarRepository carRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _carRepository = carRepository;
            _validator = new CarValidator();
        }

        #endregion Injection

        public IEnumerable<CarDTO> GetCars()
        {
            var cars = _carRepository.GetAll();
            return _mapper.Map<IEnumerable<CarDTO>>(cars);
        }

        public CarDTO GetCar(int carID)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _carRepository.GetByID(carID);
            if (car == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            return _mapper.Map<CarDTO>(car);
        }

        public void AddCar(CarDTO carDTO)
        {
            // Check if the input data is valid
            var validationResult = _validator.Validate(carDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _carRepository.Insert(_mapper.Map<Car>(carDTO));
            _carRepository.Save();
        }

        public void UpdateCar(int carID, CarDTO carDTO)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _carRepository.GetByID(carID);
            if (car == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            // Check if the input data is valid
            var validationResult = _validator.Validate(carDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _carRepository.Update(carID, _mapper.Map<Car>(carDTO));
            _carRepository.Save();
        }

        public void DeleteCar(int carID)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _carRepository.GetByID(carID);
            if (car == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            _carRepository.Delete(carID);
            _carRepository.Save();
        }

        public CarDTO UploadImage(IFormFile formFile, int carID)
        {
            if (carID < 1)
                throw new ArgumentException($"Invalid car ID: {carID}. Car ID must be greater than or equal to 1.");

            var car = _carRepository.GetByID(carID);
            if (car == null)
                throw new InvalidOperationException($"Car with ID: {carID} not found.");

            using (MemoryStream memoryStream = new())
            {
                formFile.CopyTo(memoryStream);
                car.Image = memoryStream.ToArray();
            }

            _carRepository.Update(carID, car);
            _carRepository.Save();

            return _mapper.Map<CarDTO>(car);
        }
    }
}

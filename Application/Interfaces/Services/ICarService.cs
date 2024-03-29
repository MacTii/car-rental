﻿using Application.Mapper.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICarService
    {
        public IEnumerable<CarDTO> GetCars();
        public CarDTO GetCar(int carID);
        public void AddCar(CarDTO carDTO);
        public void UpdateCar(int carID, CarDTO carDTO);
        public void DeleteCar(int carID);
        public CarDTO UploadImage(IFormFile formFile, int carID);
    }
}

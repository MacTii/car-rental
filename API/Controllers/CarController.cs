﻿using Application.Interfaces;
using Application.Mapper.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")] // Remove /Car from URL route
    [ApiController]
    public class CarController : ControllerBase
    {
        #region Injection

        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;

        public CarController(ILogger<CarController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        #endregion Injection

        [HttpGet("cars")]
        public ActionResult GetCars()
        {
            var cars = _carService.GetCars();
            if (!cars.Any())
                return NoContent(); // Returns HTTP 204 if there are no records

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Cars records retrieved successfully",
                    Data = cars
                });
        }

        [HttpGet("cars/{carID}")]
        public ActionResult GetCarByID(int carID)
        {
            var carDTO = _carService.GetCar(carID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record retrieved successfully",
                    Data = carDTO
                });
        }

        [HttpPost("cars")]
        public ActionResult AddCar(CarDTO carDTO)
        {
            if (carDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _carService.AddCar(carDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record created successfully",
                    Data = carDTO
                });
        }

        [HttpPut("cars/{carID}")]
        public ActionResult UpdateCar(int carID, CarDTO carDTO)
        {
            if (carDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            _carService.UpdateCar(carID, carDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record updated successfully",
                    Data = carDTO
                });
        }

        [HttpDelete("cars/{carID}")]
        public ActionResult DeleteCar(int carID)
        {
            _carService.DeleteCar(carID);
            return Ok(
                new
                {
                    Response = "Car record deleted successfully"
                });
        }
    }
}

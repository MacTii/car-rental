using Application.Interfaces;
using Domain.Entities;
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
        private readonly ICarRepository _carRepository;

        public CarController(ILogger<CarController> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        #endregion Injection

        [HttpGet("cars")]
        public ActionResult GetCars()
        {
            var cars = _carRepository.GetAll();
            if (!cars.Any())
            {
                return NoContent(); // Returns HTTP 204 if there are no records
            }

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
            var car = _carRepository.GetByID(carID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record retrieved successfully",
                    Data = car
                });
        }

        [HttpPost("cars")]
        public ActionResult AddCar(Car car)
        {
            if (car == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _carRepository.Insert(car);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record created successfully",
                    Data = car
                });
        }

        [HttpPut("cars/{carID}")]
        public ActionResult UpdateCar(int carID, Car car)
        {
            if (car == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _carRepository.Update(carID, car);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record updated successfully",
                    Data = car
                });
        }

        [HttpDelete("cars/{carID}")]
        public ActionResult DeleteCar(int carID)
        {
            _carRepository.Delete(carID);

            return NoContent(); // return 204 No Content
        }
    }
}

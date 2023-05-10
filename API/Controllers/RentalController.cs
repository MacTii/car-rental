using Application.Interfaces;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        #region Injection

        private readonly ILogger<RentalController> _logger;
        private readonly IRentalService _rentalService;

        public RentalController(ILogger<RentalController> logger, IRentalService rentalService)
        {
            _logger = logger;
            _rentalService = rentalService;
        }

        #endregion Injection

        [HttpGet("rentals")]
        public ActionResult GetCars()
        {
            var rentalDTOs = _rentalService.GetRentals();
            if (!rentalDTOs.Any())
            {
                return NoContent(); // Returns HTTP 204 if there are no records
            }

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Rental records retrieved successfully",
                    Data = rentalDTOs
                });
        }

        [HttpGet("rentals/{rentalID}")]
        public ActionResult GetRentalByID(int rentalID)
        {
            var rentalDTO = _rentalService.GetRental(rentalID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Rental record retrieved successfully",
                    Data = rentalDTO
                });
        }

        [HttpPost("rentals")]
        public ActionResult AddRental(RentalDTO rentalDTO)
        {
            if (rentalDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _rentalService.AddRental(rentalDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record created successfully",
                    Data = rentalDTO
                });
        }

        [HttpPut("rentals/{rentalID}")]
        public ActionResult UpdateRental(int rentalID, RentalDTO rentalDTO)
        {
            if (rentalDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _rentalService.UpdateRental(rentalID, rentalDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Car record updated successfully",
                    Data = rentalDTO
                });
        }

        [HttpDelete("rentals/{rentalID}")]
        public ActionResult DeleteCar(int rentalID)
        {
            _rentalService.DeleteRental(rentalID);
            return Ok(
                new
                {
                    Response = "Car record deleted successfully"
                });
        }
    }
}

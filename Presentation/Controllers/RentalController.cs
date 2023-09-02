using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
        }

        #endregion Injection

        [HttpGet("rentals")]
        [Authorize]
        public ActionResult GetCars()
        {
            var rentalDTOs = _rentalService.GetRentals();
            if (!rentalDTOs.Any())
                return NoContent(); // Returns HTTP 204 if there are no records

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Rental records retrieved successfully",
                    Data = rentalDTOs
                });
        }

        [HttpGet("rentals/id/{rentalID}")]
        [Authorize]
        public ActionResult GetRentalByID(int rentalID)
        {
            var rentalDTO = _rentalService.GetRentalByID(rentalID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Rental record retrieved successfully",
                    Data = rentalDTO
                });
        }

        [HttpGet("rentals/username/{username}")]
        [Authorize]
        public ActionResult GetRentalsByUsername(string username)
        {
            var rentalDTO = _rentalService.GetRentalsByUsername(username);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "Rental record retrieved successfully",
                    Data = rentalDTO
                });
        }

        [HttpPost("rentals")]
        [Authorize]
        public ActionResult AddRental(RentalDTO rentalDTO)
        {
            if (rentalDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

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
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateRental(int rentalID, RentalDTO rentalDTO)
        {
            if (rentalDTO == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRental(int rentalID)
        {
            _rentalService.DeleteRental(rentalID);
            return Ok(
                new
                {
                    Response = "Rental record deleted successfully"
                });
        }
    }
}

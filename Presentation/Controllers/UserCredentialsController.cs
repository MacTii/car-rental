using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        #region Injection

        private readonly ILogger<UserCredentialsController> _logger;
        private readonly IUserCredentialsService _userCredentialsService;

        public UserCredentialsController(ILogger<UserCredentialsController> logger, IUserCredentialsService userCredentialsService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userCredentialsService = userCredentialsService ?? throw new ArgumentNullException(nameof(userCredentialsService));
        }

        #endregion Injection

        [HttpGet("user-credentials")]
        [Authorize]
        public ActionResult GetUserCredentials()
        {
            var userCredentialsDTOs = _userCredentialsService.GetUserCredentials();
            if (!userCredentialsDTOs.Any())
            {
                return NoContent(); // Returns HTTP 204 if there are no records
            }

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User credentials records retrieved successfully",
                    Data = userCredentialsDTOs
                });
        }

        [HttpGet("user-credentials/{userCredentialsID}")]
        [Authorize]
        public ActionResult GetUserCredentialsByID(int userCredentialsID)
        {
            var user = _userCredentialsService.GetUserCredential(userCredentialsID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User credentials record retrieved successfully",
                    Data = user
                });
        }

        [HttpPost("user-credentials")]
        [Authorize]
        public ActionResult AddUserCredential(UserCredentialsDTO userCredentialsDTO)
        {
            if (userCredentialsDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userCredentialsService.AddUserCredential(userCredentialsDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User credentials record created successfully",
                    Data = userCredentialsDTO
                });
        }

        [HttpPut("user-credentials/{userCredentialsID}")]
        [Authorize]
        public ActionResult UpdateUserCredential(int userCredentialsID, UserCredentialsDTO userCredentialsDTO)
        {
            if (userCredentialsDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userCredentialsService.UpdateUserCredential(userCredentialsID, userCredentialsDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User credentials record updated successfully",
                    Data = userCredentialsDTO
                });
        }

        [HttpDelete("user-credentials/{userCredentialsID}")]
        [Authorize]
        public ActionResult DeleteUserCredential(int userCredentialsID)
        {
            _userCredentialsService.DeleteUserCredential(userCredentialsID);
            return Ok(
                new
                {
                    Response = "User credentials record deleted successfully"
                });
        }
    }
}

using Application.Interfaces;
using Application.Mapper.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")] // Remove /User from URL route
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Injection

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        #endregion Injection

        [HttpGet("users")]
        public ActionResult GetUsers()
        {
            var usersDTO = _userService.GetUsers();
            if (!usersDTO.Any())
            {
                return NoContent(); // Returns HTTP 204 if there are no records
            }

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User records retrieved successfully",
                    Data = usersDTO
                });
        }

        [HttpGet("users/{userID}")]
        public ActionResult GetUserByID(int userID)
        {
            var user = _userService.GetUser(userID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record retrieved successfully",
                    Data = user
                });
        }

        [HttpPost("users")]
        public ActionResult AddUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userService.AddUser(userDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record created successfully",
                    Data = userDTO
                });
        }

        [HttpPut("users/{userID}")]
        public ActionResult UpdateUser(int userID, UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userService.UpdateUser(userID, userDTO);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record updated successfully",
                    Data = userDTO
                });
        }

        [HttpDelete("users/{userID}")]
        public ActionResult DeleteUser(int userID)
        {
            _userService.DeleteUser(userID);
            return Ok(
                new
                {
                    Response = "User record deleted successfully"
                });
        }
    }
}
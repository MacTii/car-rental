using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")] // Remove /User from URL route
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public ActionResult GetUsers()
        {
            var users = _userRepository.GetAll();
            if (!users.Any())
            {
                return NoContent(); // Returns HTTP 204 if there are no records
            }

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User records retrieved successfully",
                    Data = users
                });
        }

        [HttpGet("users/{userID}")]
        public ActionResult GetUserByID(int userID)
        {
            var user = _userRepository.GetByID(userID);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record retrieved successfully",
                    Data = user
                });
        }

        [HttpPost("users")]
        public ActionResult AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userRepository.Insert(user);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record created successfully",
                    Data = user
                });
        }

        [HttpPut("users/{userID}")]
        public ActionResult UpdateUser(int userID, User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid input data"); // return 400 Bad Request
            }

            _userRepository.Update(userID, user);

            // HTTP 200
            return Ok(
                new
                {
                    Response = "User record updated successfully",
                    Data = user
                });
        }

        [HttpDelete("users/{userID}")]
        public ActionResult DeleteUser(int userID)
        {
            _userRepository.Delete(userID);

            return NoContent(); // return 204 No Content
        }
    }
}
using Application.Interfaces;
using Application.Mapper.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService, IUserCredentialsService userCredentialsService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet("username")]
        [Authorize]
        public ActionResult GetMe()
        {
            var username = _authService.GetMyUsername();
            return Ok(username);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterDTO request)
        {
            if (request == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            var user = _authService.Register(request);
            return Ok(
                new
                {
                    Response = "User registered successfully",
                    Data = user
                });
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDTO request)
        {
            if (request == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            var token = _authService.Login(request);
            return Ok(
                new
                {
                    Response = "User logged in successfully",
                    Data = token
                });
        }

        [HttpPost("refresh-token")]
        public ActionResult RefreshToken()
        {
            var token = _authService.RefreshToken();
            return Ok(
                new
                {
                    Response = "Token refreshed successfully",
                    Data = token
                });
        }
    }
}

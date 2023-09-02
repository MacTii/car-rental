using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Injection

        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService, IUserCredentialsService userCredentialsService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        #endregion Injection

        [HttpGet("username")]
        [Authorize]
        public ActionResult GetMe()
        {
            var username = _authService.GetMyUsername();
            return Ok(username);
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterDTO request)
        {
            if (request == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            var token = _authService.Register(request);
            return Ok(
                new
                {
                    Response = "User registered successfully",
                    Data = token
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
        [Authorize]
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

        [HttpGet("generate-password-credentials")]
        [Authorize(Roles = "Admin")]
        public ActionResult GeneratePasswordCredentials()
        {
            var passwordCredentials = _authService.GetPasswordCredentials();
            return Ok(
                new
                {
                    Response = "Password credentials generated successfully",
                    Data = passwordCredentials
                });
        }

        [HttpGet("reset-password-credentials/{userID}")]
        [Authorize(Roles ="Admin")]
        public ActionResult ResetPassword(int userID)
        {
            _authService.ResetPassword(userID);
            return Ok(
                new
                {
                    Response = "Password reset successful",
                });
        }
    }
}

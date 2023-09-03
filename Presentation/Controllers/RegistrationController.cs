using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class RegistrationController : ControllerBase
    {
        #region Injection

        private readonly IConfiguration _configuration;
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IConfiguration configuration, IRegistrationService registrationService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
        }

        #endregion Injection

        [HttpPost("register")]
        public ActionResult Register(RegisterDTO request)
        {
            if (request == null)
                return BadRequest("Invalid input data"); // return 400 Bad Request

            var token = _registrationService.Register(request);
            return Ok(
                new
                {
                    Response = "User registered successfully",
                    Data = token
                });
        }
    }
}

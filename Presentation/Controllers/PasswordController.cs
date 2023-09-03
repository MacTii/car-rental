using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")] // Remove /Comment from URL route
    [ApiController]
    public class PasswordController : ControllerBase
    {
        #region Injection

        private readonly ILogger<CommentController> _logger;
        private readonly IPasswordService _passwordService;

        public PasswordController(ILogger<CommentController> logger, IPasswordService passwordService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        #endregion Injection

        [HttpGet("generate-password-credentials")]
        [Authorize(Roles = "Admin")]
        public ActionResult GeneratePasswordCredentials()
        {
            var passwordCredentials = _passwordService.GetPasswordCredentials();
            return Ok(
                new
                {
                    Response = "Password credentials generated successfully",
                    Data = passwordCredentials
                });
        }

        [HttpGet("reset-password-credentials/{userID}")]
        [Authorize(Roles = "Admin")]
        public ActionResult ResetPassword(int userID)
        {
            _passwordService.ResetPassword(userID);
            return Ok(
                new
                {
                    Response = "Password reset successful",
                });
        }
    }
}

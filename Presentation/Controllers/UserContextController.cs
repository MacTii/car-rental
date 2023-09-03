using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserContextController : ControllerBase
    {
        #region Injection

        private readonly IConfiguration _configuration;
        private readonly IUserContextService _userContextService;

        public UserContextController(IConfiguration configuration, IUserContextService userContextService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        #endregion Injection

        [HttpGet("username")]
        [Authorize]
        public ActionResult GetMe()
        {
            var username = _userContextService.GetMyUsername();
            return Ok(username);
        }
    }
}

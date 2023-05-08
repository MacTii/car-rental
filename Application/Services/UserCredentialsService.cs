using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.Interfaces;

namespace Application.Services
{
    public class UserCredentialsService : IUserCredentialsService
    {
        /*private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCredentialsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }*/

        public string GetMyName()
        {
            /*var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;*/
            return string.Empty;
        }
    }
}

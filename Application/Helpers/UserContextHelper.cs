using Application.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class UserContextHelper : IUserContextHelper
    {
        #region Injection

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Injection

        public string GetMyUsername()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                Claim? claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
                result = claim != null ? claim.Value : string.Empty;
            }
            return result;
        }
    }
}

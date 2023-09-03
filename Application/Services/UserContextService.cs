using Application.Interfaces.Helpers;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserContextService : IUserContextService
    {
        #region Injection

        private readonly IUserContextHelper _userContextHelper;

        public UserContextService(IUserContextHelper userContextHelper)
        {
            _userContextHelper = userContextHelper;
        }

        #endregion Injection

        public string GetMyUsername()
        {
            string username = _userContextHelper.GetMyUsername();
            return username;
        }
    }
}

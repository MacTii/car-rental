using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Helpers
{
    public interface ITokenHelper
    {
        public string GetToken(UserCredentials userCredentials);
        public RefreshToken GenerateRefreshToken();
    }
}

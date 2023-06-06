using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Helpers
{
    public interface IAuthenticationHelper
    {
        public RefreshToken GenerateRefreshToken();
        public void SetRefreshToken(RefreshToken newRefreshToken, UserCredentials user);
        public string CreateToken(UserCredentials user);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}

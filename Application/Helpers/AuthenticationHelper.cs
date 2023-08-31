using Application.Interfaces.Helpers;
using Domain.Entities;
using Domain.Models;

namespace Application.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        #region Injection

        private readonly IPasswordHelper _passwordHelper;
        private readonly ITokenHelper _tokenHelper;

        public AuthenticationHelper(IPasswordHelper passwordHelper, ITokenHelper tokenHelper)
        {
            _passwordHelper = passwordHelper;
            _tokenHelper = tokenHelper;
        }

        #endregion Injection

        // PASSWORD HELPER
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            _passwordHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return _passwordHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        }

        public PasswordCredential GenerateRandomPasswordCredential()
        {
            return _passwordHelper.GenerateRandomPasswordCredential();
        }

        // TOKEN HELPER
        public string GetToken(UserCredentials userCredentials)
        {
            return _tokenHelper.GetToken(userCredentials);
        }

        public RefreshToken GenerateRefreshToken()
        {
            return _tokenHelper.GenerateRefreshToken();
        }
    }
}

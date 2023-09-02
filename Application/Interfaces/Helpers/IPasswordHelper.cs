

using Domain.Models;

namespace Application.Interfaces.Helpers
{
    public interface IPasswordHelper
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public PasswordCredential GenerateRandomPasswordCredential();
    }
}

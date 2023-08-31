using Application.Interfaces.Helpers;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        #region Injection

        private readonly ILogger<PasswordHelper> _logger;

        public PasswordHelper(ILogger<PasswordHelper> logger)
        {
            _logger = logger;
        }

        #endregion Injection

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public PasswordCredential GenerateRandomPasswordCredential()
        {
            var randomPassword = GenerateRandomPassword(10);

            // Save password to LOG (only testing purpose - no email functionality)
            _logger.LogInformation(randomPassword);

            CreatePasswordHash(randomPassword, out byte[] passwordHash, out byte[] passwordSalt);

            var passwordCredential = new PasswordCredential()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Get PasswordHash and PasswordSalt
            return passwordCredential;
        }

        private static string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+"; // Chars to choose
            Random random = new();
            StringBuilder password = new();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                password.Append(chars[index]);
            }

            return password.ToString();
        }
    }
}

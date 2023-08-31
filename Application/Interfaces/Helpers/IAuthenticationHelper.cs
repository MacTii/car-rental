﻿using Domain.Entities;
using Domain.Models;

namespace Application.Interfaces.Helpers
{
    public interface IAuthenticationHelper
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public PasswordCredential GenerateRandomPasswordCredential();
        public string GetToken(UserCredentials userCredentials);
        public RefreshToken GenerateRefreshToken();
    }
}

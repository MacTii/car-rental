using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public UserCredentialsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Injection

        public IEnumerable<UserCredentials> GetAll()
        {
            var userCredentials = _context.UserCredentials.ToList();
            return userCredentials;
        }

        public UserCredentials GetByID(int userCredentialsID)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. User credentials ID must be greater " +
                    $"than or equal to 1.");

            var userCredentials = _context.UserCredentials.Find(userCredentialsID);
            return userCredentials ?? throw new InvalidOperationException($"User credentials with ID: {userCredentialsID} not found.");
        }

        public UserCredentials? GetByUsername(string username)
        {
            var userCredentials = _context.UserCredentials.FirstOrDefault(e => e.Username == username);
            return userCredentials;
        }

        public void Insert(UserCredentials userCredentials)
        {
            _context.UserCredentials.Add(userCredentials);
        }

        public void Update(int userCredentialsID, UserCredentials userCredentials)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. Car ID must be greater than or equal to 1.");

            var existingUserCredentials = _context.UserCredentials.Find(userCredentialsID);
            if (existingUserCredentials == null)
                throw new InvalidOperationException($"Car with ID: {userCredentialsID} not found.");

            existingUserCredentials.Username = userCredentials.Username;
            existingUserCredentials.PasswordHash = userCredentials.PasswordHash;
            existingUserCredentials.PasswordSalt = userCredentials.PasswordSalt;
            existingUserCredentials.RefreshToken = userCredentials.RefreshToken;
            existingUserCredentials.TokenCreated = userCredentials.TokenCreated;
            existingUserCredentials.TokenExpires = userCredentials.TokenExpires;

            _context.Entry(existingUserCredentials).State = EntityState.Modified;
        }

        public void Delete(int userCredentialsID)
        {
            if (userCredentialsID < 1)
                throw new ArgumentException($"Invalid user credentials ID: {userCredentialsID}. User credentials ID must " +
                    $"be greater than or equal to 1.");

            var userCredentials = _context.UserCredentials.Find(userCredentialsID);
            if (userCredentials == null)
                throw new InvalidOperationException($"User credentials with ID: {userCredentialsID} not found.");

            _context.UserCredentials.Remove(userCredentials);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

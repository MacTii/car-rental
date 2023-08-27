using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.Include(x => x.UserCredentials).ToList();
            return users;
        }

        public User GetByID(int userID)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _context.Users.Find(userID);
            return user ?? throw new InvalidOperationException($"User with ID: {userID} not found.");
        }

        public User GetByUsername(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var user = _context.Users
                .Where(x => x.UserCredentials.Username == username)
                .SingleOrDefault();

            return user ?? throw new InvalidOperationException($"User with username: {username} not found.");
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(int userID, User user)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var existingUser = _context.Users.Find(userID) ?? throw new InvalidOperationException($"User with ID: {userID} not found.");

            CopyProperties(user, existingUser);

            _context.Entry(existingUser).State = EntityState.Modified;
        }

        public void Delete(int userID)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _context.Users.Find(userID);
            if (user == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            _context.Users.Remove(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private static void CopyProperties(object source, object destination)
        {
            Type type = source.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(destination, value);
                }
            }
        }
    }
}

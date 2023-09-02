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

        public User? GetByID(int userID)
        {
            var user = _context.Users
                .Include(x => x.UserCredentials)
                .SingleOrDefault(x => x.ID == userID);

            return user;
        }

        public User? GetByUsername(string username)
        {
            var user = _context.Users
                .Include(x => x.UserCredentials)
                .SingleOrDefault(x => x.UserCredentials.Username == username);

            return user;
        }

        public User? GetByEmail(string email)
        {
            var user = _context.Users
                .Include(x => x.UserCredentials)
                .SingleOrDefault(x => x.Email == email);

            return user;
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(int userID, User user)
        {
            var existingUser = _context.Users.Single(x => x.ID == userID);
            CopyProperties(user, existingUser);

            _context.Entry(existingUser).State = EntityState.Modified;
        }

        public void Delete(int userID)
        {
            var user = _context.Users.Single(x => x.ID == userID);
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

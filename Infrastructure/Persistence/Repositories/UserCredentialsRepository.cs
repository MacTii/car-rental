using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public IEnumerable<UserCredentials> GetAll()
        {
            var userCredentials = _context.UserCredentials.ToList();
            return userCredentials;
        }

        public UserCredentials? GetByID(int userCredentialsID)
        {
            var userCredentials = _context.UserCredentials.Find(userCredentialsID);
            return userCredentials;
        }

        public UserCredentials? GetByUsername(string username)
        {
            var userCredentials = _context.UserCredentials.SingleOrDefault(e => e.Username == username);
            return userCredentials;
        }

        public void Insert(UserCredentials userCredentials)
        {
            _context.UserCredentials.Add(userCredentials);
        }

        public void Update(int userCredentialsID, UserCredentials userCredentials)
        {
            var existingUserCredentials = _context.UserCredentials.Single(x => x.ID == userCredentialsID);
            CopyProperties(userCredentials, existingUserCredentials);

            _context.Entry(existingUserCredentials).State = EntityState.Modified;
        }

        public void Delete(int userCredentialsID)
        {
            var userCredentials = _context.UserCredentials.Single(x=> x.ID == userCredentialsID);
            _context.UserCredentials.Remove(userCredentials);
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

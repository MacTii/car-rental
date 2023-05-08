using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalContext _context;

        public UserRepository(CarRentalContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetByID(int userID)
        {
            if(userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var user = _context.Users.Find(userID);
            return user ?? throw new InvalidOperationException($"User with ID: {userID} not found.");
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(int userID, User user)
        {
            if (userID < 1)
                throw new ArgumentException($"Invalid user ID: {userID}. User ID must be greater than or equal to 1.");

            var existingUser = _context.Users.Find(userID);
            if (existingUser == null)
                throw new InvalidOperationException($"User with ID: {userID} not found.");

            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

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
    }
}

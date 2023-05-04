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
            var user = _context.Users.Find(userID);
            return user ?? throw new ArgumentException("Invalid user ID");
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(int userID, User user)
        {
            var existingUser = _context.Users.Find(userID);
            if (existingUser == null)
            {
                throw new ArgumentException($"User with ID {userID} not found.");
            }

            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;

            _context.Entry(existingUser).State = EntityState.Modified;
        }

        public void Delete(int userID)
        {
            var user = _context.Users.Find(userID);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

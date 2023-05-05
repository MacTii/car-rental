using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User GetUser(int userID);
        public void AddUser(User user);
        public void UpdateUser(int userID, User user);
        public void DeleteUser(int userID);
    }
}

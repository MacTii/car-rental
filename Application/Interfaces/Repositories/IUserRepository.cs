using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public User GetByID(int userID);
        public User GetByUsername(string username);
        public void Insert(User user);
        public void Update(int userID, User user);
        public void Delete(int userID);
        public void Save();
    }
}

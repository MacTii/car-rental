using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserCredentialsRepository
    {
        public IEnumerable<UserCredentials> GetAll();
        public UserCredentials GetByID(int userCredentialsID);
        public UserCredentials? GetByUsername(string username);
        public void Insert(UserCredentials userCredentials);
        public void Update(int userCredentialsID, UserCredentials userCredentials);
        public void Delete(int userCredentialsID);
        public void Save();
    }
}

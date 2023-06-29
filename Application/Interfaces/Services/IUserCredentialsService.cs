using Application.Mapper.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserCredentialsService
    {
        public IEnumerable<UserCredentialsDTO> GetUserCredentials();
        public UserCredentialsDTO GetUserCredential(int userCredentialsID);
        public void AddUserCredential(UserCredentialsDTO userCredentialsDTO);
        public void UpdateUserCredential(int userCredentialsID, UserCredentialsDTO userCredentialsDTO);
        public void DeleteUserCredential(int userCredentialsID);
    }
}

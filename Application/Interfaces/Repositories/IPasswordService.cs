using Application.Mapper.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPasswordService
    {
        public PasswordCredentialDTO GetPasswordCredentials();
        public void ResetPassword(int userID);
    }
}

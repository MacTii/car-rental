using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBackupRestoreService
    {
        public bool RestoreBackup(IFormFile backupFile, string databaseName);
    }
}

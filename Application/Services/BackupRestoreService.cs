using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BackupRestoreService : IBackupRestoreService
    {
        #region Injection

        private readonly IRawQueryRepository _rawQueryRepository;

        public BackupRestoreService(IRawQueryRepository rawQueryRepository)
        {
            _rawQueryRepository = rawQueryRepository;
        }

        #endregion Injection

        public bool RestoreBackup(IFormFile backupFile, string databaseName)
        {
            // Create a temporary file and get its path
            string tempFilePath = Path.GetTempFileName();

            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                backupFile.CopyTo(stream);
            }

            // USE [master]
            // RESTORE DATABASE [CarRental] FROM  DISK = N'G:\Mateusz\PROGRAMMING\VS\CarRental\Backup\CarRentalBackup_2023-09-03.bak' WITH  FILE = 1,  NOUNLOAD,  STATS = 5
            // GO

            // USE [master]
            // RESTORE DATABASE[CarRental123] FROM DISK = N'G:\Mateusz\PROGRAMMING\VS\CarRental\Backup\CarRentalBackup_2023-09-03.bak' WITH FILE = 1, MOVE N'CarRental' TO N'C:\Users\MacTii\CarRental123.mdf',  MOVE N'CarRental_log' TO N'C:\Users\MacTii\CarRental123_log.ldf',  NOUNLOAD,  STATS = 5
            // GO

            // -- Terminate all sessions related to the CarRental database
            _rawQueryRepository.ExecuteSqlRaw($"USE [master] ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

            // Execute SQL command to restore the database from the backup file
            _rawQueryRepository.ExecuteSqlRaw($"USE [master] RESTORE DATABASE [{databaseName}] FROM DISK = N'{tempFilePath}' WITH  FILE = 1,  NOUNLOAD,  STATS = 5");

            // -- Restore access to the CarRental database
            _rawQueryRepository.ExecuteSqlRaw($"USE [master] ALTER DATABASE [{databaseName}] SET MULTI_USER");

            // Delete the temporary file
            File.Delete(tempFilePath);

            return true; // Database restore completed successfully
        }
    }
}

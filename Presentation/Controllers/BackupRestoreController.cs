using Application.Interfaces.Services;
using Application.Mapper.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class BackupRestoreController : ControllerBase
    {
        #region Injection

        private readonly IConfiguration _configuration;
        private readonly IBackupRestoreService _backupRestoreService;

        public BackupRestoreController(IConfiguration configuration, IBackupRestoreService backupRestoreService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _backupRestoreService = backupRestoreService ?? throw new ArgumentNullException(nameof(backupRestoreService));
        }

        #endregion Injection

        [HttpPost("restore-database")]
        public ActionResult RestoreDatabase(IFormFile backupFile)
        {
            if (backupFile == null || backupFile.Length == 0)
                return BadRequest("No file selected for backup restore.");

            string databaseName = _configuration.GetValue<string>("DatabaseName");
            bool restoreResult = _backupRestoreService.RestoreBackup(backupFile, databaseName);

            if (restoreResult)
                return Ok($"Database restored from backup: {backupFile.FileName}");
            else
                return BadRequest("Database restore failed.");
        }
    }
}

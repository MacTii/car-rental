using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class RawQueryRepository : IRawQueryRepository
    {
        #region Injection

        private readonly ApplicationDbContext _context;

        public RawQueryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Injection

        public void ExecuteSqlRaw(string sql)
        {
            _context.Database.ExecuteSqlRaw(sql);
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        public DbSet<Car> Cars { get; }
        public DbSet<User> Users { get; }
        public DbSet<Rental> Rentals { get; }
        public DbSet<UserCredentials> UserCredentials { get; }
        public DbSet<Blog> Blogs { get; }
        public DbSet<Comment> Comments { get; }
    }
}

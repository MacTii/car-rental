using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Domain.Entities;

namespace Infrastructure
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
        }

        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRental;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

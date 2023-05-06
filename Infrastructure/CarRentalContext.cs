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
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRental;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    //.IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    //.IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    //.IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Make)
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .HasMaxLength(100);

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(10);

            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Domain.Entities;
using Domain.Enums;

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
        public DbSet<UserCredentials> UserCredentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRental;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date");

                entity.Property(e => e.Gender)
                    .HasConversion(
                        v => v.ToString(),
                        v => (GenderEnum)Enum.Parse(typeof(GenderEnum), v));

                entity.Property(e => e.IdentificationNumber)
                    .HasMaxLength(15);

                entity.Property(e => e.DrivingLicenseNumber)
                    .HasMaxLength(20);

            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Make)
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .HasMaxLength(100);

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(15);

                entity.Property(e => e.Color)
                    .HasConversion(
                        v => v.ToString(),
                        v => (ColorEnum)Enum.Parse(typeof(ColorEnum), v));

                entity.Property(e => e.PricePerDay)
                    .HasPrecision(5, 2);

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasDefaultValue(true);

            });

            modelBuilder.Entity<Rental>(entity =>
                entity.ToTable(t => t.HasCheckConstraint("CK_Rental_Dates", "ReturnDate >= RentDate")));

            modelBuilder.Entity<UserCredentials>(entity =>
            {
                entity.Property(e => e.Username)
                    .HasMaxLength(10);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(e => e.UserRole)
                    .HasConversion(
                        v => v.ToString(),
                        v => (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), v));
            });
        }
    }
}

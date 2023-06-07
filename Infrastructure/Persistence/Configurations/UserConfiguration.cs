using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
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
        }
    }
}

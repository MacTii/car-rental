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
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> entity)
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
        }
    }
}

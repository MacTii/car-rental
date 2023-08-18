using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> entity)
        {
            entity.ToTable(t => t.HasCheckConstraint("CK_Rental_Dates", "ReturnDate >= RentDate"));
            
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20);
        }
    }
}

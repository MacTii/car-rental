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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50);

            entity.Property(e => e.Surname)
                .HasMaxLength(50);

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(300);
        }
    }
}

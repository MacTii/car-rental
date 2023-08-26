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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> entity)
        {
            entity.Property(e => e.Title)
                .HasMaxLength(50);

            entity.Property(e => e.AuthorName)
                .HasMaxLength(50);

            entity.Property(e => e.AuthorSurname)
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(250);

            entity.Property(e => e.DetailedDescription)
                .HasMaxLength(700);
        }
    }
}

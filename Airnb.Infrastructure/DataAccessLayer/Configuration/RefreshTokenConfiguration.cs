using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.DataAccessLayer.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(t => t.TokenHash)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasIndex(t => t.TokenHash)
                .IsUnique();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(t => t.IsActive);
        }
    }
}

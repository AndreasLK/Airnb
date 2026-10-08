using Microsoft.EntityFrameworkCore;
using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.DataAccessLayer.Configuration
{
    public class HostConfiguration : IEntityTypeConfiguration<HostProfile>
    {
        public void Configure(EntityTypeBuilder<HostProfile> builder)
        {
            builder.HasOne<User>().WithOne().HasForeignKey<HostProfile>(h => h.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(h => h.Role).HasConversion<string>();

        }
    }
}

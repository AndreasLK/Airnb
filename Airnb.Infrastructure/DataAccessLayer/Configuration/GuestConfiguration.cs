using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Airnb.Infrastructure.DataAccessLayer.Configuration
{
    public class GuestConfiguration : IEntityTypeConfiguration<GuestProfile>
    {
        public void Configure(EntityTypeBuilder<GuestProfile> builder)
        {
            builder.OwnsOne(g => g.Address);

            builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<GuestProfile>(g => g.UserId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete when a User is deleted, preserving the GuestProfile.
        }
    }
}

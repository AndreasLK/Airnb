using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airnb.Infrastructure.DataAccessLayer.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.Property(b => b.Status)
                .HasConversion<string>();

            builder.OwnsOne(b => b.TimeRange, tr =>
            {
                tr.Property(x => x.Start).HasColumnName("StartTime");
                tr.Property(x => x.End).HasColumnName("EndTime");
            });

            builder.OwnsOne(b => b.Reciept, r =>
            {
                r.OwnsOne(x => x.StartPrice, m =>
                {
                    m.Property(x => x.Amount).HasColumnName("PriceAmount").HasColumnType("decimal(18,2)");
                    m.Property(x => x.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
                });

                r.Property(x => x.Service).HasColumnName("Service").HasMaxLength(200);
            });

            builder.HasOne<Home>()
            .WithMany()         //har en booking en home, men en home kan have mange bookings
            .HasForeignKey(b => b.HomeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<GuestProfile>()
            .WithMany()         //har en booking en guestprofile, men en guestprofile kan have mange bookings
            .HasForeignKey(b => b.GuestProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.DataAccessLayer.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Status)
                .HasConversion<string>()
                .IsRequired();

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
        }
    }
}
using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Airnb.Infrastructure.DataAccessLayer.Configuration
{
    public class HomeConfiguration : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.Property(h => h.HomeType).HasConversion<string>();

            builder.OwnsOne(h => h.Address);
            builder.OwnsOne(h => h.HomeFeatures);

            builder.OwnsOne(h => h.PricePerDay, m =>
            {
                m.Property(x => x.Amount).HasColumnName("PriceAmount").HasColumnType("decimal(18,2)");
                m.Property(x => x.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
            });
        }
    }
}

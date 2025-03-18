using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class StatItemConfiguration : IEntityTypeConfiguration<StatItem>
    {
        public void Configure(EntityTypeBuilder<StatItem> builder)
        {
            builder.HasKey(si => si.Id);
            builder.Property(si => si.Key).IsRequired();
            builder.Property(si => si.Value).IsRequired();
        }
    }
}
using BarberShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class HeroSectionConfiguration : IEntityTypeConfiguration<HeroSection>
{
    public void Configure(EntityTypeBuilder<HeroSection> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Title).IsRequired().HasMaxLength(255);
        builder.Property(h => h.Subtitle).HasMaxLength(255);
        builder.Property(h => h.ImageUrl).IsRequired().HasMaxLength(2048);
        // Configure the table name if different from the default
        builder.ToTable("HeroSections");
    }
}
using BarberShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CtaSectionConfiguration : IEntityTypeConfiguration<CtaSection>
{
    public void Configure(EntityTypeBuilder<CtaSection> builder)
    {
        builder.ToTable("CtaSections");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Message)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.ButtonText)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.ButtonUrl)
            .IsRequired()
            .HasMaxLength(255);
    }
}
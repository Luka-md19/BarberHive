using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class FooterSectionConfiguration : IEntityTypeConfiguration<FooterSection>
{
    public void Configure(EntityTypeBuilder<FooterSection> builder)
    {
        builder.HasKey(f => f.Id);

        // Configure SiteLinks property
        builder.Property(f => f.SiteLinks)
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
               );

        // Configure PolicyDocuments property
        builder.Property(f => f.PolicyDocuments)
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
               );

        // Configure Address property
        builder.Property(f => f.Address)
               .IsRequired();

        // Configure ContactInfo property
        builder.Property(f => f.ContactInfo)
               .IsRequired();
    }
}

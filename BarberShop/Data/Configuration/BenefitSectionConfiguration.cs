using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BarberShop.Data.Configuration
{
    public class BenefitSectionConfiguration : IEntityTypeConfiguration<BenefitSection>
    {
        public void Configure(EntityTypeBuilder<BenefitSection> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Benefits)
                .IsRequired()
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}

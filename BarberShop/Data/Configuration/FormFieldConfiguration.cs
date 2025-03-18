using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class FormFieldConfiguration : IEntityTypeConfiguration<FormField>
{
    public void Configure(EntityTypeBuilder<FormField> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name).IsRequired();
        builder.Property(f => f.Type).IsRequired();

        // Configure Options property if necessary
        builder.Property(f => f.Options)
               .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
               );

        // Configure the foreign key relationship
        builder.HasOne(f => f.ContactFormSection)
               .WithMany(c => c.Fields)
               .HasForeignKey(f => f.ContactFormSectionId);
    }
}

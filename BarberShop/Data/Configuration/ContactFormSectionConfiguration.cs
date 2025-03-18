using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContactFormSectionConfiguration : IEntityTypeConfiguration<ContactFormSection>
{
    public void Configure(EntityTypeBuilder<ContactFormSection> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.HasMany(c => c.Fields).WithOne().HasForeignKey(f => f.ContactFormSectionId);
    }
}

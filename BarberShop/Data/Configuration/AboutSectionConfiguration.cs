using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AboutSectionConfiguration : IEntityTypeConfiguration<AboutSection>
{
    public void Configure(EntityTypeBuilder<AboutSection> builder)
    {
        builder.ToTable("AboutSections"); // Specify the table name

        // Configure the primary key
        builder.HasKey(a => a.Id);

        // Configure properties
        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(100); // Set a maximum length for the Title property

        builder.Property(a => a.Content)
            .IsRequired()
            .HasColumnType("nvarchar(max)"); // Use a suitable data type for the Content property
    }
}
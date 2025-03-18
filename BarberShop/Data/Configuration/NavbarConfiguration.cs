using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public partial class NavbarConfiguration : IEntityTypeConfiguration<Navbar>
{
    public void Configure(EntityTypeBuilder<Navbar> builder)
    {
        builder.HasKey(n => n.Id);

        // Sections can be configured as a value conversion to store as a JSON string
        builder.Property(n => n.Sections)
            .IsRequired()
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));

        // Define the relationship between Navbar and NavbarAction
        builder.HasMany(n => n.Actions)
            .WithOne(a => a.Navbar)
            .HasForeignKey(a => a.NavbarId);
    }
}
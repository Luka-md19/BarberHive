using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public partial class NavbarConfiguration
{
    public class NavbarActionConfiguration : IEntityTypeConfiguration<NavbarAction>
    {
        public void Configure(EntityTypeBuilder<NavbarAction> builder)
        {
            builder.HasKey(a => a.Id);

            // Items can be configured as a value conversion if needed, similar to Sections
            builder.Property(a => a.Items)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null))
                .IsRequired(false); // Make it optional since not all actions will have dropdown items

            builder.HasOne(a => a.Navbar)
                .WithMany(n => n.Actions)
                .HasForeignKey(a => a.NavbarId);
        }
    }
}
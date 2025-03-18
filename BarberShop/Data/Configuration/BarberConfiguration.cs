using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class BarberConfiguration : IEntityTypeConfiguration<Barber>
    {
        public void Configure(EntityTypeBuilder<Barber> builder)
        {
            builder.HasKey(b => b.Id); // Primary Key

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.Description)
                .IsRequired(false) // Making Description optional
                .HasMaxLength(1000);

            builder.Property(b => b.CalendlyLink)
                .IsRequired(false) // Making CalendlyLink optional
                .HasMaxLength(255); // Specifying MaxLength for consistency, assuming URL

            builder.Property(b => b.PhotoUrl)
                .IsRequired(false) // Making PhotoUrl optional
                .HasMaxLength(255); // Assuming URL, so MaxLength is set for consistency

            builder.Property(b => b.Position)
                .IsRequired(false) // Making Position optional
                .HasMaxLength(255); // Adjust length as needed for position titles

            // Relationship with BarberService
            builder.HasMany(b => b.BarberServices)
                .WithOne(bs => bs.Barber)
                .HasForeignKey(bs => bs.BarberId);

            builder.HasOne(b => b.TeamSection)
               .WithMany(ts => ts.TeamMembers)
               .HasForeignKey(b => b.TeamSectionId);

            builder.ToTable("Barbers");
        }
    }
}

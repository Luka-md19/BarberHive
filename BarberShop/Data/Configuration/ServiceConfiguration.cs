using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.Description)
                .IsRequired(false);

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Configuring new fields
            builder.Property(s => s.Category)
                .IsRequired(false) // Category is optional
                .HasMaxLength(255); // Assuming a max length, adjust as needed

            builder.Property(s => s.Duration)
                .IsRequired(); // Assuming every service has a fixed duration

            builder.Property(s => s.IsActive)
                .IsRequired(); // IsActive flag to indicate service availability

            builder.HasMany(s => s.BarberServices)
                .WithOne(bs => bs.Service)
                .HasForeignKey(bs => bs.ServiceId);

            builder.ToTable("Services");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class BarberServiceConfiguration : IEntityTypeConfiguration<BarberService>
    {
        public void Configure(EntityTypeBuilder<BarberService> builder)
        {
            // Composite primary key configuration
            builder.HasKey(bs => new { bs.BarberId, bs.ServiceId });

            // Barber to BarberService relationship
            builder.HasOne(bs => bs.Barber)
                .WithMany(b => b.BarberServices) // Assuming Barber contains a collection of BarberServices
                .HasForeignKey(bs => bs.BarberId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            // Service to BarberService relationship
            builder.HasOne(bs => bs.Service)
                .WithMany(s => s.BarberServices) // Assuming Service contains a collection of BarberServices
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            builder.Property(b => b.BarberShopId)
              .IsRequired();

            // Optional: Configure indexes for performance optimization
            builder.HasIndex(bs => bs.BarberId);
            builder.HasIndex(bs => bs.ServiceId);

            builder.ToTable("BarberServices"); // Maps to the BarberServices table
        }
    }
}

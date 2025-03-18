using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id); // Primary Key

            // CalendlyEventId property configuration
            builder.Property(a => a.CalendlyEventId)
                .IsRequired()
                .HasMaxLength(255); // Assuming Calendly event IDs fit within this length; adjust as necessary

            // Customer (ApiUser) relationship configuration remains the same
            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Appointments) // Link to the collection of appointments in ApiUser
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Optional: If appointments are directly tied to barbers through Calendly
            builder.HasOne(a => a.Barber)
                .WithMany() // Assuming no navigation property in Barber for appointments; adjust if necessary
                .HasForeignKey(a => a.BarberId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes if a barber is removed

            // Service relationship might become optional or unnecessary depending on how service details are managed in Calendly
            builder.HasOne(a => a.Service)
                .WithMany() // Adjust based on actual navigation property presence
                .HasForeignKey(a => a.ServiceId)
                .IsRequired(false) // ServiceId can be null if services are managed through Calendly
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            // Appointment start and end times
            builder.Property(a => a.AppointmentStart)
                .IsRequired();

            builder.Property(a => a.AppointmentEnd)
                .IsRequired();

            // Status property configuration
            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.ToTable("Appointments"); // Maps to the Appointments table
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class ContactInquiryConfiguration : IEntityTypeConfiguration<ContactInquiry>
    {
        public void Configure(EntityTypeBuilder<ContactInquiry> builder)
        {
            builder.HasKey(ci => ci.Id); // Primary Key

            builder.Property(ci => ci.FullName)
                .IsRequired() // Not nullable
                .HasMaxLength(255); // Maximum length

            builder.Property(ci => ci.Email)
                .IsRequired() // Not nullable
                .HasMaxLength(255) // Maximum length
                .HasAnnotation("EmailAddress", true); // Ensure EmailAddress validation if supported

            builder.Property(ci => ci.Message)
                .IsRequired(); // Not nullable

            builder.Property(ci => ci.Status)
                .IsRequired(false) // Making it optional
                .HasMaxLength(50); // Maximum length for status

            builder.Property(ci => ci.Response)
                .IsRequired(false); // Optional response field

            builder.ToTable("ContactInquiries"); // Maps to the ContactInquiries table
        }
    }
}

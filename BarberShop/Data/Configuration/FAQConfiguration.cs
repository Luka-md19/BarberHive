using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Data.Configuration
{
    public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            builder.HasKey(f => f.Id); // Primary Key

            builder.Property(f => f.Question)
                .IsRequired()
                .HasMaxLength(255); // Set maximum length for the question

            builder.Property(f => f.Answer)
                .IsRequired(); // Answer is required but without a set maximum length

            builder.Property(f => f.Category)
                .IsRequired(false) // Category is optional
                .HasMaxLength(50); // Set maximum length for the category, if provided

            builder.ToTable("FAQs"); // Maps to the FAQs table
        }
    }
}

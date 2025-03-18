using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TestimonialConfiguration : IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(t => t.DatePosted)
            .IsRequired();

        // Add configuration for FirstName property
        builder.Property(t => t.FirstName)
            .IsRequired(false) // Make this optional, assuming a testimonial might not always have a linked user.
            .HasMaxLength(255); // Define a max length according to your needs

        // Relationship to ApiUser
        builder.HasOne(t => t.Customer)
            .WithMany() // Assuming you don't have a navigation property in ApiUser for Testimonials
            .HasForeignKey(t => t.CustomerId)
            .IsRequired(false); // Assuming the relationship is optional

        // Relationship to Service
        builder.HasOne(t => t.Service)
            .WithMany() // Assuming you don't have a navigation property in Service for Testimonials
            .HasForeignKey(t => t.ServiceId)
            .IsRequired(false); // Assuming the relationship is optional

        builder.ToTable("Testimonials");
    }
}

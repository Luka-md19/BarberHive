using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BarberShop.Data.Configuration
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(bp => bp.Id); // Primary Key

            builder.Property(bp => bp.Title)
                .IsRequired() // Not nullable
                .HasMaxLength(255); // Maximum length of 255 characters

            builder.Property(bp => bp.Content)
                .IsRequired(); // Not nullable

            builder.Property(bp => bp.DatePosted)
                .IsRequired(); // Not nullable
                               // Consider using .HasColumnType("datetime2") for more precision if needed

            builder.Property(bp => bp.Category)
                .IsRequired(false) // Optional
                .HasMaxLength(255); // Maximum length of 255 characters if categories are used

            builder.ToTable("BlogPosts"); // Maps to the BlogPosts table
        }
    }
}

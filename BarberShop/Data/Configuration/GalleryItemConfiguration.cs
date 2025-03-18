using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public partial class GalleryItemConfiguration : IEntityTypeConfiguration<GalleryItem>
{
    public void Configure(EntityTypeBuilder<GalleryItem> builder)
    {
        builder.HasKey(gi => gi.Id);

        builder.Property(gi => gi.ImageUrl)
            .IsRequired()
            .HasAnnotation("Url", true); // Note: EF Core does not enforce URL validation at the database level.

        builder.Property(gi => gi.Caption)
            .IsRequired(false);

        builder.Property(gi => gi.DateAdded)
            .IsRequired();

        builder.ToTable("GalleryItems");
    }
}

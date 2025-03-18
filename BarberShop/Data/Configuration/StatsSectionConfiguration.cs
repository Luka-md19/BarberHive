using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class StatsSectionConfiguration : IEntityTypeConfiguration<StatsSection>
{
    public void Configure(EntityTypeBuilder<StatsSection> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Title).IsRequired();
        builder.HasMany(s => s.StatItems)
            .WithOne(si => si.StatsSection)
            .HasForeignKey(si => si.StatsSectionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
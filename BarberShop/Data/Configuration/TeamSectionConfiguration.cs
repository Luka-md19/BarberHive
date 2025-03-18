using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class TeamSectionConfiguration : IEntityTypeConfiguration<TeamSection>
{
    public void Configure(EntityTypeBuilder<TeamSection> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired();
        builder.Property(t => t.BarberShopId)
           .IsRequired();


        builder.HasMany(t => t.TeamMembers)
               .WithOne(b => b.TeamSection) 
               .HasForeignKey(b => b.TeamSectionId);

      
    }
}

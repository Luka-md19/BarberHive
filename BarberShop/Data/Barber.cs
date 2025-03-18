using BarberShop.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Barber : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } // Could be repurposed as Bio

    [Url]
    [MaxLength(255)]
    public string CalendlyLink { get; set; }

    public virtual ICollection<BarberService> BarberServices { get; set; } = new List<BarberService>();

    [Url]
    public string PhotoUrl { get; set; }

    [MaxLength(255)]
    public string Position { get; set; } // New field

    // Foreign key
    public int TeamSectionId { get; set; }
    public TeamSection TeamSection { get; set; } // Navigation property

    [Required]
    public Guid BarberShopId { get; set; }
}

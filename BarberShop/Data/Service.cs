using BarberShop.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Service : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; } // Making this nullable as per previous design

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

   
    public string Category { get; set; } 
    public int Duration { get; set; }
    public bool IsActive { get; set; } = true; 

    public virtual ICollection<BarberService> BarberServices { get; set; } = new List<BarberService>();

    [Required]
    public Guid BarberShopId { get; set; }
}

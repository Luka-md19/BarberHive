using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class GalleryItem : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Url]
    public string ImageUrl { get; set; }

    public string Caption { get; set; }
    public DateTime DateAdded { get; set; }

    [MaxLength(255)]
    public string Category { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

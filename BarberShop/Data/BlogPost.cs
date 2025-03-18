using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class BlogPost : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public DateTime DatePosted { get; set; }

    public string Category { get; set; }
    [Required]
    public Guid BarberShopId { get; set; }
}

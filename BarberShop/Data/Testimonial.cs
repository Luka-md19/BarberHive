using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class Testimonial : ITenant
{
    [Key]
    public int Id { get; set; }

    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public virtual ApiUser Customer { get; set; }
 

    [Required]
    [MaxLength(1000)]
    public string Text { get; set; }

    public DateTime DatePosted { get; set; }

    public int? ServiceId { get; set; }
    public virtual Service Service { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

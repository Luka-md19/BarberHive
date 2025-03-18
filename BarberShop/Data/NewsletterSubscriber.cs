using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class NewsletterSubscriber : ITenant
{
    [Key]
    public string Email { get; set; }
    public bool IsSubscribed { get; set; } = true;

    [Required]
    public Guid BarberShopId { get; set; }


}

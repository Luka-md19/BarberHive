using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.NewsletterSubscriberDtos
{
    public abstract class BaseNewsletterSubscriberDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Guid BarberShopId { get; set; }

    }
}

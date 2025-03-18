namespace BarberShop.Models.NewsletterSubscriberDtos
{
    public class UpdateNewsletterSubscriberDto : BaseNewsletterSubscriberDto
    {
        public bool IsSubscribed { get; set; } = true;
    }
}

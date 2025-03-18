namespace BarberShop.Models.BlogPostDtos
{
    public class BlogPostDto : BaseBlogPostDto
    {
        public int Id { get; set; }
        public DateTime DatePosted { get; set; }
    }
}

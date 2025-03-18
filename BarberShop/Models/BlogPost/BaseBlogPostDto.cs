using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.BlogPostDtos
{
    public abstract class BaseBlogPostDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Category { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

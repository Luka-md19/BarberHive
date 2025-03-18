using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.AboutSectionDtos
{
    public abstract class BaseAboutSectionDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid BarberShopId { get; set; }


    }
}

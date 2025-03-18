// BaseHeroSectionDto.cs
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.HeroSectionDtos
{
    public abstract class BaseHeroSectionDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Subtitle { get; set; }

        [Required]
        [Url]
        [MaxLength(2048)] // URLs can be lengthy
        public string ImageUrl { get; set; }

        public Guid BarberShopId { get; set; }
    }
}





using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Data.Entities
{
    public class HeroSection : ITenant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Subtitle { get; set; }

        [Required]
        [Url]
        [MaxLength(2048)] // URLs can be long, adjusting accordingly
        public string ImageUrl { get; set; }

        [Required]
        public Guid BarberShopId { get; set; }
    }
}

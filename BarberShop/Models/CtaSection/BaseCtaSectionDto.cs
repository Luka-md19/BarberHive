using System.ComponentModel.DataAnnotations;

// Base DTO for shared properties
namespace BarberShop.Models.CtaSection
{
    public abstract class BaseCtaSectionDto
    {
        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string ButtonText { get; set; }

        [Required]
        [MaxLength(255)]
        public string ButtonUrl { get; set; }

        public Guid BarberShopId { get; set; }
    }
}


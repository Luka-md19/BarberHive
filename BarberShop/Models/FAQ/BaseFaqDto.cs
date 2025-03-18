using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.FaqDtos
{
    public abstract class BaseFaqDto
    {
        [Required]
        [MaxLength(255)]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

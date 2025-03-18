using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.ServiceDtos
{
    public abstract class BaseServiceDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public Guid BarberShopId { get; set; }
    }
}

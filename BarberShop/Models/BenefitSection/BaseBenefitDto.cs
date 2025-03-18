using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.BenefitSectionDtos
{
    public abstract class BaseBenefitDto
    {
        [Required]
        public string Title { get; set; }

        public List<string> Benefits { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.TestimonialDtos
{
    public abstract class BaseTestimonialDto
    {

        public string FirstName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
        public int? ServiceId { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

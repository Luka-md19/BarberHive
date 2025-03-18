using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.ContactInquiryDtos
{
    public abstract class BaseContactInquiryDto
    {
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

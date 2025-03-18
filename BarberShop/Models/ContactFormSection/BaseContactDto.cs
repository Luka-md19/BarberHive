using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.ContactSectionDtos
{
    public abstract class BaseContactDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Url]
        public string PhotoUrl { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

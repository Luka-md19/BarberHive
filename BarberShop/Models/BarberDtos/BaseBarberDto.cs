using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.BarberDtos
{
    public abstract class BaseBarberDto
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Url]
        public string PhotoUrl { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }
        public int TeamSectionId { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

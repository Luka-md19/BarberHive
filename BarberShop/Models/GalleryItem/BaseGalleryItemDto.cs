using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.GalleryItemDtos
{
    public abstract class BaseGalleryItemDto
    {
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public string Caption { get; set; }

        public Guid BarberShopId { get; set; }

    }
}


// BaseStatItemDto.cs
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.StatItemDtos
{
    public abstract class BaseStatItemDto
    {

        public int StatsSectionId { get; set; }
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public Guid BarberShopId { get; set; }




    }
}





using BarberShop.Models.StatItemDtos;
using Newtonsoft.Json;

namespace BarberShop.Models.StatsDtos
{
    public class StatsSectionDto : BaseStatsSectionDto
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }


        [JsonProperty(Order = 3)]
        public ICollection<StatItemDto> StatItems { get; set; } // Serialize third
    }
}

using Newtonsoft.Json;

namespace BarberShop.Models.TeamDtos
{
    public abstract class BaseTeamSectionDto
    {
        [JsonProperty(Order = 1)]
        public string Title { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

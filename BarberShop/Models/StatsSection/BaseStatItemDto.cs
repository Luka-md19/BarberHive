using System.Collections.Generic;
using BarberShop.Models.StatsDtos;
using Newtonsoft.Json;

namespace BarberShop.Models.StatsDtos
{
    public abstract class BaseStatsSectionDto
    {
        [JsonProperty(Order = 2)]
        public string Title { get; set; }
        public Guid BarberShopId { get; set; }
    }
}

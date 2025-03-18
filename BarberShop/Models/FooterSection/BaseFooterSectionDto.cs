using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarberShop.Models.FooterSectionDtos
{
    public abstract class BaseFooterSectionDto
    {
        public List<string> SiteLinks { get; set; }

        public List<string> PolicyDocuments { get; set; }

        public string Address { get; set; }

        public string ContactInfo { get; set; }

        public Guid BarberShopId { get; set; }
    }

}

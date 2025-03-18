using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarberShop.Models.FormFieldDtos
{

    public abstract class BaseFormFieldDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public bool Required { get; set; }

        public List<string> Options { get; set; }

        public int ContactFormSectionId { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

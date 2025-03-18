using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Data
{
    public class CtaSection : ITenant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string ButtonText { get; set; }

        [Required]
        [MaxLength(255)]
        public string ButtonUrl { get; set; }

        [Required]
        public Guid BarberShopId { get; set; }
    }
}

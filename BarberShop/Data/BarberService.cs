using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class BarberService : ITenant
{
    [Key]
    public int Id { get; set; }

    public int BarberId { get; set; }
    public virtual Barber Barber { get; set; }

    public int ServiceId { get; set; }
    public virtual Service Service { get; set; }
    [Required]
    public Guid BarberShopId { get; set; }
}

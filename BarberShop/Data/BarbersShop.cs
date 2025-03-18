using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class BarbersShop : ITenant
{
    public int Id { get; set; }
    public Guid BarberShopId { get; set; }
    public string Name { get; set; }

}

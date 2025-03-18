using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class Navbar : ITenant
{
    [Key]
    public int Id { get; set; }

    public List<string> Sections { get; set; }

    
    public List<NavbarAction> Actions { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

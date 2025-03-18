using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class NavbarAction : ITenant
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Items { get; set; } 
    public int NavbarId { get; set; }
    public Navbar Navbar { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}
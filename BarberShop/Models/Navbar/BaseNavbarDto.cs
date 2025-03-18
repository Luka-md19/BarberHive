using BarberShop.Models.NavbarAction;
using BarberShop.Models.NavbarDtos;
using System.ComponentModel.DataAnnotations;

public abstract class BaseNavbarDto
    {
    public List<string> Sections { get; set; }
    public Guid BarberShopId { get; set; }


}

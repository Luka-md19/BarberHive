using System.ComponentModel.DataAnnotations;

public abstract class BaseNavbarActionDto
    {
    public string Name { get; set; }
    public List<string> Items { get; set; }

    public int NavbarId { get; set; }
    public Guid BarberShopId { get; set; }

}

using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class StatsSection : ITenant
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public ICollection<StatItem> StatItems { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}
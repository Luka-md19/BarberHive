using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class StatItem : ITenant
{
    public int Id { get; set; }
    public int StatsSectionId { get; set; }

    [Required]
    public string Key { get; set; }

    [Required]
    public string Value { get; set; }

    public StatsSection StatsSection { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}
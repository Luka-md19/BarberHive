using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class BenefitSection : ITenant
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }
    public List<string> Benefits { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class FooterSection : ITenant
{
    [Key]
    public int Id { get; set; }

    public List<string> SiteLinks { get; set; } 
    public List<string> PolicyDocuments { get; set; } 
    public string Address { get; set; } 
    public string ContactInfo { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}
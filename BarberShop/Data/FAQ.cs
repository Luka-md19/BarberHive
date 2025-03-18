using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class FAQ : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Question { get; set; }

    [Required]
    public string Answer { get; set; }

    [MaxLength(50)]
    public string Category { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

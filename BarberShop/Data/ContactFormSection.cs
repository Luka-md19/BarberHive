using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class ContactFormSection : ITenant
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<FormField> Fields { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

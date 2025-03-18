using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class FormField : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Type { get; set; } // e.g., "text", "email", "select"

    public bool Required { get; set; }
    public List<string> Options { get; set; } // For 'select' type fields

    // Add the foreign key property
    public int ContactFormSectionId { get; set; }
    public ContactFormSection ContactFormSection { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

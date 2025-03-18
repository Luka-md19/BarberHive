using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class ContactInquiry : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Message { get; set; }

    
    [MaxLength(50)]
    public string Status { get; set; } // For example: "Pending", "Answered", "Closed"

    public string Response { get; set; } // Optional field for storing a response if needed

    [Required]
    public Guid BarberShopId { get; set; }
}

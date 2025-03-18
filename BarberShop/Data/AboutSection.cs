using System;
using System.ComponentModel.DataAnnotations;
using BarberShop.Repository; 

public class AboutSection : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

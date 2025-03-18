using BarberShop.Repository;
using System.ComponentModel.DataAnnotations;

public class TeamSection : ITenant
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }
    public virtual ICollection<Barber> TeamMembers { get; set; }

    [Required]
    public Guid BarberShopId { get; set; }
}

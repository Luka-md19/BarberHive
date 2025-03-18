namespace BarberShop.Models.StatsDtos
{
    public class UpdateStatsSectionDto : BaseStatsSectionDto
    {
        public int Id { get; set; }
        // Consider whether StatItems need to be updated as part of this DTO
    }
}

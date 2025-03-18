namespace BarberShop.Models.StatItemDtos
{
    public class GetStatItemDto : BaseStatItemDto
    {
        public int Id { get; set; }
        public int StatsSectionId { get; set; }
    }
}
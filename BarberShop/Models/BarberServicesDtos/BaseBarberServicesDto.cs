namespace BarberShop.Models.BarberServicesDtos
{
    public abstract class BaseBarberServicesDto
    {
        public int BarberId { get; set; }
        public int ServiceId { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

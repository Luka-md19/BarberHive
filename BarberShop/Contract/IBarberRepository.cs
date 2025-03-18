using BarberShop.Models.BarberDtos;
using BarberShop.Models.StatsDtos;
using BarberShop.Models.TeamDtos;

namespace BarberShop.Contract
{
    public interface IBarberRepository : IGenericRepository<Barber>
    {
        Task<List<BarberDto>> GetBarbersByShopIdAsync(Guid barberShopId);
    }
}


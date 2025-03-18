using BarberShop.Models.NavbarDtos;

namespace BarberShop.Contract
{
    public interface INavbarRepository : IGenericRepository<Navbar>
    {
        Task<NavbarDto> GetNavbarAsync(int id, Guid barberShopId);
    }
}

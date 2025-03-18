using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Models.NavbarDtos;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Repository
{
    public partial class NavbarRepository : GenericRepository<Navbar>, INavbarRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public NavbarRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NavbarDto> GetNavbarAsync(int id, Guid barberShopId)

        {
            var navbar = await _context.Navbars
                .Include(n => n.Actions)
                .FirstOrDefaultAsync(n => n.Id == id); // Fixed: Changed navbarId to id

            if (navbar == null)
            {
                return null;
            }

            return _mapper.Map<NavbarDto>(navbar); // This line might also need attention
        }


    }
}
using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public partial class NavbarRepository
    {
        public class NavbarActionRepository : GenericRepository<NavbarAction>, INavbarActionRepository
        {
            private readonly BarberShopDbContext _context;
            private readonly IMapper _mapper;

            public NavbarActionRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
            {
                _context = context;
                _mapper = mapper;
            }
        }
    }
}

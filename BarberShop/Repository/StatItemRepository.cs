using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class StatItemRepository : GenericRepository<StatItem>, IStatItemRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public StatItemRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

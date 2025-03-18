using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class BarberServiceRepository : GenericRepository<BarberService>, IBarberServiceRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public BarberServiceRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}


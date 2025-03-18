using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class FooterSectionRepository : GenericRepository<FooterSection>, IFooterSectionRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public FooterSectionRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

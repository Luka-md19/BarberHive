using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public FAQRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}

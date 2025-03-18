using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Data.Entities;

namespace BarberShop.Repository
{
    public class AboutSectionRepository : GenericRepository<AboutSection>, IAboutSectionRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public AboutSectionRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

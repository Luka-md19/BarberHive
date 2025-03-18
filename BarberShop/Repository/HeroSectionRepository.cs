using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberShop.Repository
{
    public class HeroSectionRepository : GenericRepository<HeroSection>, IHeroSectionRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public HeroSectionRepository(BarberShopDbContext context , IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}

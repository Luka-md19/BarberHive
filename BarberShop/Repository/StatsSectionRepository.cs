using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Models.StatsDtos;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Repository
{
    public class StatsSectionRepository : GenericRepository<StatsSection>, IStatsSectionRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public StatsSectionRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StatsSectionDto> GetStatsSectionAsync(int id)
        {
            var statsSection = await _context.StatsSections
                .Include(ss => ss.StatItems) 
                .FirstOrDefaultAsync(ss => ss.Id == id);

            if (statsSection == null)
            {
                return null;
            }

            return _mapper.Map<StatsSectionDto>(statsSection);
        }

    }
}

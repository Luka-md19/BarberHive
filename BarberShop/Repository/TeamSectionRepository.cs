using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Models.TeamDtos;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Repository
{
    public class TeamSectionRepository : GenericRepository<TeamSection>, ITeamSectionRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public TeamSectionRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamSectionDto> GetTeamSectionAsync(int id)
        {
            var TeamSection = await _context.TeamSections
         .Include(ss => ss.TeamMembers)
         .FirstOrDefaultAsync(ss => ss.Id == id);

            if (TeamSection == null)
            {
                return null;
            }

            return _mapper.Map<TeamSectionDto>(TeamSection);
        }
        public async Task<List<TeamSection>> GetAllTeamSectionsAsync(Guid barberShopId)
        {
            return await _context.TeamSections
                                 .Where(t => t.BarberShopId == barberShopId)
                                 .ToListAsync();
        }

    }
}

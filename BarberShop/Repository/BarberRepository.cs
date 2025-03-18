using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Models.BarberDtos;
using BarberShop.Models.StatsDtos;
using BarberShop.Models.TeamDtos;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Repository
{
    public class BarberRepository : GenericRepository<Barber>, IBarberRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public BarberRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        // Inside BarberRepository
        public async Task<List<BarberDto>> GetBarbersByShopIdAsync(Guid barberShopId)
        {
            if (barberShopId == Guid.Empty)
            {
                throw new ArgumentException("BarberShopId cannot be empty.");
            }

            var barbers = await _context.Barbers
                                        .Where(b => b.BarberShopId == barberShopId)
                                        .ToListAsync();

            if (!barbers.Any())
            {
                // Handle the case where no barbers are found.
                return new List<BarberDto>();
            }

            var barberDtos = _mapper.Map<List<BarberDto>>(barbers);
            return barberDtos;
        }
    }
}

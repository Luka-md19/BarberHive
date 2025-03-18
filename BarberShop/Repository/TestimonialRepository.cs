using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Models.TeamDtos;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Repository
{
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public TestimonialRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

    
    }
}

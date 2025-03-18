using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public BlogPostRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}

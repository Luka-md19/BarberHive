using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class NewsletterSubscriberRepository : GenericRepository<NewsletterSubscriber>, INewsletterSubscriberRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public NewsletterSubscriberRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}

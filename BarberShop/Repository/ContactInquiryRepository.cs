using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class ContactInquiryRepository : GenericRepository<ContactInquiry>, IContactInquiryRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public ContactInquiryRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}

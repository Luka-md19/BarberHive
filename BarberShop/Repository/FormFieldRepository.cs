using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;

namespace BarberShop.Repository
{
    public class FormFieldRepository : GenericRepository<FormField>, IFormFieldRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public FormFieldRepository(BarberShopDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

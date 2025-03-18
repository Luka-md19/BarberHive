using AutoMapper;
using BarberShop.Data;
using BarberShop.Models.CtaSection;
using BarberShop.Models.FaqDtos;
using BarberShop.Models.FooterSectionDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.NavbarDtos;
using BarberShop.Models.ServiceDtos;
using BarberShop.Models.TestimonialDtos;
using BarberShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.Models.BenefitSectionDtos;

namespace BarberShop.Repository
{
    public class ServicesPageRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public ServicesPageRepository(BarberShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServicesPageDto> GetServicesPageDataAsync()
        {
          
            var heroSection = await _context.HeroSections.FirstOrDefaultAsync();
            var services = await _context.Services.ToListAsync();
            var testimonials = await _context.Testimonials.ToListAsync();
            var benefits = await _context.BenefitSections.ToListAsync();
            var faqs = await _context.FAQs.ToListAsync();
            var ctaSection = await _context.CtaSections.FirstOrDefaultAsync();
         

            // Map to DTOs
            var servicesPageDto = new ServicesPageDto
            {
               
                HeroSection = _mapper.Map<HeroSectionDto>(heroSection),
                Services = _mapper.Map<List<ServiceDto>>(services),
                Testimonials = _mapper.Map<List<TestimonialDto>>(testimonials),
                Benefits = _mapper.Map<List<BenefitDto>>(benefits),
                FAQs = _mapper.Map<List<FaqDto>>(faqs),
                CTA = _mapper.Map<CtaSectionDto>(ctaSection),
                
            };

            return servicesPageDto;
        }
    }
}

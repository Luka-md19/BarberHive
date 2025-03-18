using AutoMapper;
using BarberShop.Data;
using BarberShop.Models;
using BarberShop.Models.CtaSection;
using BarberShop.Models.FaqDtos;
using BarberShop.Models.FooterSectionDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.HomePage;
using BarberShop.Models.NavbarDtos;
using BarberShop.Models.NewsletterSubscriberDtos;
using BarberShop.Models.ServiceDtos;
using BarberShop.Models.TeamDtos;
using BarberShop.Models.TestimonialDtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberShop.Repository
{
    public class HomePageRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public HomePageRepository(BarberShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HomePageDto> GetHomePageDataAsync()
        {
          
            var heroSection = await _context.HeroSections.FirstOrDefaultAsync();
            var services = await _context.Services.ToListAsync();
            var ctaSection = await _context.CtaSections.FirstOrDefaultAsync();
            var testimonials = await _context.Testimonials.ToListAsync();
            var faqs = await _context.FAQs.ToListAsync();
            var team = await _context.TeamSections.Include(ss => ss.TeamMembers).FirstOrDefaultAsync();

            // Map to DTOs
            var homePageDto = new HomePageDto
            {
             
                HeroSection = _mapper.Map<HeroSectionDto>(heroSection),
                Services = _mapper.Map<List<ServiceDto>>(services),
                CTA = _mapper.Map<CtaSectionDto>(ctaSection),
                Testimonials = _mapper.Map<List<TestimonialDtos>>(testimonials),
                FAQs = _mapper.Map<List<FaqDto>>(faqs),
                TeamSection = _mapper.Map<TeamSectionDto>(team),


            };

            return homePageDto;
        }
    }
}

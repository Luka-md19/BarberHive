using AutoMapper;
using BarberShop.Data;
using BarberShop.Models.AboutSectionDtos;
using BarberShop.Models.AboutUsPage;
using BarberShop.Models.CtaSection;
using BarberShop.Models.GalleryItemDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.HomePage;
using BarberShop.Models.StatsDtos;
using BarberShop.Models.TeamDtos;
using BarberShop.Models.TestimonialDtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberShop.Repository
{
    public class AboutUsRepository
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public AboutUsRepository(BarberShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AboutUsDto> GetAboutUsDataAsync()
        {
            // Load data from database, possibly including related entities as needed
            var heroSection = await _context.HeroSections.FirstOrDefaultAsync();
            var aboutSection = await _context.AboutSections.FirstOrDefaultAsync();
            var team = await _context.TeamSections.Include(ss=>ss.TeamMembers).FirstOrDefaultAsync();
            var statsSection = await _context.StatsSections.Include(ss => ss.StatItems).FirstOrDefaultAsync();
            var gallerySection = await _context.GalleryItems.ToListAsync();
            var testimonialSection = await _context.Testimonials.ToListAsync();
            var ctaSection = await _context.CtaSections.FirstOrDefaultAsync();

            // Map to DTOs
            var aboutUsDto = new AboutUsDto
            {
                HeroSection = _mapper.Map<HeroSectionDto>(heroSection),
                AboutSection = _mapper.Map<AboutSectionDto>(aboutSection),
                TeamSection = _mapper.Map<TeamSectionDto>(team),
                StatsSection = _mapper.Map<StatsSectionDto>(statsSection),
                GallerySection = _mapper.Map<IEnumerable<GalleryItemDto>>(gallerySection),
                TestimonialSection = _mapper.Map<IEnumerable<TestimonialDto>>(testimonialSection),
                CtaSection = _mapper.Map<CtaSectionDto>(ctaSection),
            };

            return aboutUsDto;
        }
    }
}

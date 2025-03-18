using BarberShop.Models.CtaSection;
using BarberShop.Models.TestimonialDtos;
using BarberShop.Models.AboutSectionDtos;
using BarberShop.Models.GalleryItemDtos;
using BarberShop.Models.HomePage;
using BarberShop.Models.StatsDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.TeamDtos;
// Add any other using directives needed for your sections

namespace BarberShop.Models.AboutUsPage
{
    public class AboutUsDto
    {
        public HeroSectionDto HeroSection { get; set; }
        public AboutSectionDto AboutSection { get; set; }
        public TeamSectionDto TeamSection { get; set; }
        public StatsSectionDto StatsSection { get; set; }
        public IEnumerable<GalleryItemDto> GallerySection { get; set; }
        public IEnumerable<TestimonialDto> TestimonialSection { get; set; }
        public CtaSectionDto CtaSection { get; set; }
    }
}

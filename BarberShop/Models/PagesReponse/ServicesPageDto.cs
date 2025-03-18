using BarberShop.Models.BenefitSectionDtos;
using BarberShop.Models.CtaSection;
using BarberShop.Models.FaqDtos;
using BarberShop.Models.FooterSectionDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.NavbarDtos;
using BarberShop.Models.ServiceDtos;
using BarberShop.Models.TestimonialDtos;

namespace BarberShop.Models
{
    public class ServicesPageDto
    {
       
        public HeroSectionDto HeroSection { get; set; } 
        public List<ServiceDto> Services { get; set; }
        public List<TestimonialDto> Testimonials { get; set; }
        public List<BenefitDto> Benefits { get; set; }
        public List<FaqDto> FAQs { get; set; }
        public CtaSectionDto CTA { get; set; }
       
    }
}

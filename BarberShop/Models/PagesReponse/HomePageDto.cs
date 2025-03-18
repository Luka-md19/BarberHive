using BarberShop.Models.CtaSection;
using BarberShop.Models.FaqDtos;
using BarberShop.Models.FooterSectionDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.NavbarDtos;
using BarberShop.Models.NewsletterSubscriberDtos;
using BarberShop.Models.ServiceDtos;
using BarberShop.Models.TeamDtos;


namespace BarberShop.Models.HomePage
{
        public class HomePageDto
        {

           
            public HeroSectionDto HeroSection { get; set; }
            public List<ServiceDto> Services { get; set; }
            public CtaSectionDto CTA { get; set; }
            public List<TestimonialDtos> Testimonials { get; set; }
            public List<FaqDto> FAQs { get; set; }
            public TeamSectionDto TeamSection { get; set; }
          

           
            
        }

    }


using AutoMapper;
using BarberShop.Data;
using BarberShop.Data.Configuration;
using BarberShop.Data.Entities;
using BarberShop.Models.AboutSectionDtos;
using BarberShop.Models.AppointmentDtos;
using BarberShop.Models.BarberDtos;
using BarberShop.Models.BarberServicesDtos;
using BarberShop.Models.BenefitSectionDtos;
using BarberShop.Models.BlogPostDtos;
using BarberShop.Models.ContactInquiryDtos;
using BarberShop.Models.ContactSectionDtos;
using BarberShop.Models.CtaSection;
using BarberShop.Models.FaqDtos;
using BarberShop.Models.FooterSectionDtos;
using BarberShop.Models.FormFieldDtos;
using BarberShop.Models.GalleryItemDtos;
using BarberShop.Models.HeroSection;
using BarberShop.Models.HeroSectionDtos;
using BarberShop.Models.HomePage;
using BarberShop.Models.NavbarAction;
using BarberShop.Models.NavbarDtos;
using BarberShop.Models.NewsletterSubscriberDtos;
using BarberShop.Models.ServiceDtos;
using BarberShop.Models.StatItemDtos;
using BarberShop.Models.StatsDtos;
using BarberShop.Models.TeamDtos;
using BarberShop.Models.TestimonialDtos;
using BarberShop.Models.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberShop.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, GetAppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDto>().ReverseMap();


            CreateMap<Barber, BarberDto>().ReverseMap();
            CreateMap<Barber, GetBarberDto>().ReverseMap();
            CreateMap<Barber, UpdateBarberDto>().ReverseMap();
            CreateMap<Barber, CreateBarberDto>().ReverseMap();

            CreateMap<BarberService, CreateBarberServicesDto>().ReverseMap();
            CreateMap<BarberService, BarberServicesDto>().ReverseMap();
            CreateMap<BarberService, GetBarberServicesDto>().ReverseMap();
            CreateMap<BarberService, UpdateBarberServicesDto>().ReverseMap();


            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, GetServiceDto>().ReverseMap();
            CreateMap<Service, UpdateServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();

            CreateMap<Navbar, NavbarActionDto>(); 
            CreateMap<HeroSection, HeroSectionDto>(); 
            CreateMap<Testimonial, TestimonialDto>(); 
            CreateMap<BenefitSection, BenefitDto>(); 
            CreateMap<FAQ, FaqDto>(); 
            CreateMap<CtaSection, CtaSectionDto>(); 
            CreateMap<FooterSection, FooterSectionDto>(); 

            // FAQ mappings
            CreateMap<FAQ, FaqDto>().ReverseMap();
            CreateMap<FAQ, CreateFaqDto>().ReverseMap();
            CreateMap<FAQ, GetFaqDto>().ReverseMap();
            CreateMap<FAQ, UpdateFaqDto>().ReverseMap();

            // ContactInquiry mappings
            CreateMap<ContactInquiry, ContactInquiryDto>().ReverseMap();
            CreateMap<ContactInquiry, CreateContactInquiryDto>().ReverseMap();
            CreateMap<ContactInquiry, GetContactInquiryDto>().ReverseMap();
            CreateMap<ContactInquiry, UpdateContactInquiryDto>().ReverseMap();

            // BlogPost mappings
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            CreateMap<BlogPost, CreateBlogPostDto>().ReverseMap();
            CreateMap<BlogPost, GetBlogPostDto>().ReverseMap();
            CreateMap<BlogPost, UpdateBlogPostDto>().ReverseMap();

            // GalleryItem mappings
            CreateMap<GalleryItem, GalleryItemDto>().ReverseMap();
            CreateMap<GalleryItem, CreateGalleryItemDto>().ReverseMap();
            CreateMap<GalleryItem, GetGalleryItemDto>().ReverseMap();
            CreateMap<GalleryItem, UpdateGalleryItemDto>().ReverseMap();

            // NewsletterSubscriber mappings
            CreateMap<NewsletterSubscriber, NewsletterSubscriberDto>().ReverseMap();
            CreateMap<NewsletterSubscriber, CreateNewsletterSubscriberDto>().ReverseMap();
            CreateMap<NewsletterSubscriber, GetNewsletterSubscriberDto>().ReverseMap();
            CreateMap<NewsletterSubscriber, UpdateNewsletterSubscriberDto>().ReverseMap();

            // Testimonial mappings
            CreateMap<Testimonial, TestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();

            // HeroSection mappings
            CreateMap<HeroSection, HeroSectionDto>().ReverseMap();
            CreateMap<HeroSection, CreateHeroSectionDto>().ReverseMap();
            CreateMap<HeroSection, GetHeroSectionDto>().ReverseMap();
            CreateMap<HeroSection, UpdateHeroSectionDto>().ReverseMap();

            // CtaSection mappings
            CreateMap<CtaSection, CtaSectionDto>().ReverseMap();
            CreateMap<CtaSection, CreateCtaSectionDto>().ReverseMap();
            CreateMap<CtaSection, GetCtaSectionDto>().ReverseMap();
            CreateMap<CtaSection, UpdateCtaSectionDto>().ReverseMap();

      
            CreateMap<HeroSection, HeroSectionDto>();
            CreateMap<Service, ServiceDto>();
            CreateMap<CtaSection, CtaSectionDto>();
            CreateMap<Testimonial, TestimonialDtos>();
            CreateMap<FAQ, FaqDto>();
            CreateMap<TeamSection, TeamSectionDto>();


            CreateMap<HeroSection, HeroSectionDto>().ReverseMap();
            CreateMap<AboutSection, AboutSectionDto>().ReverseMap();
            CreateMap<TeamSection, TeamSectionDto>().ReverseMap();
            CreateMap<StatsSection, StatsSectionDto>().ReverseMap();
            CreateMap<GalleryItem, GalleryItemDto>().ReverseMap();
            CreateMap<Testimonial, TestimonialDtos>().ReverseMap();
            CreateMap<CtaSection, CtaSectionDto>().ReverseMap();

            CreateMap<StatsSection, StatsSectionDto>()
    .ForMember(dest => dest.StatItems, opt => opt.MapFrom(src => src.StatItems));

            CreateMap<StatsSection, CreateStatsSectionDto>().ReverseMap();
            CreateMap<StatsSection, GetStatsSectionDto>().ReverseMap();
            CreateMap<StatsSection, UpdateStatsSectionDto>().ReverseMap();

        
            CreateMap<StatItem, StatItemDto>().ReverseMap();
            CreateMap<StatItem, CreateStatItemDto>().ReverseMap();
            CreateMap<StatItem, GetStatItemDto>().ReverseMap();
            CreateMap<StatItem, UpdateStatItemDto>().ReverseMap();


            CreateMap<TeamSection, TeamSectionDto>()
     .ForMember(dest => dest.TeamMembers, opt => opt.MapFrom(src => src.TeamMembers));
            CreateMap<TeamSection, CreateTeamSectionDto>().ReverseMap();
            CreateMap<TeamSection, GetTeamSectionDto>().ReverseMap();
            CreateMap<TeamSection, UpdateTeamSectionDto>().ReverseMap();

            // Repeat this pattern for any other entities and their corresponding DTOs.


            CreateMap<Navbar, NavbarDto>()
     .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions))
     .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections));
            CreateMap<Navbar, CreateNavbarDto>().ReverseMap();
            CreateMap<Navbar, GetNavbarDto>().ReverseMap();
            CreateMap<Navbar, UpdateNavbarDto>().ReverseMap();

            CreateMap<NavbarAction, NavbarActionDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<NavbarAction, GetNavbarActionDto>().ReverseMap();
            CreateMap<NavbarAction, CreateNavbarActionDto>().ReverseMap();
            CreateMap<NavbarAction, NavbarActionItemDto>().ReverseMap();


            CreateMap<BenefitSection, BenefitDto>().ReverseMap();
            CreateMap<BenefitSection, CreateBenefitDto>().ReverseMap();
            CreateMap<BenefitSection, GetBenefitDto>().ReverseMap();
            CreateMap<BenefitSection, UpdateBenefitDto>().ReverseMap();

            CreateMap<ContactFormSection, ContactDto>().ReverseMap();
            CreateMap<ContactFormSection, CreateContactDto>().ReverseMap();
            CreateMap<ContactFormSection, GetContactDto>().ReverseMap();
            CreateMap<ContactFormSection, UpdateContactDto>().ReverseMap();

            CreateMap<FooterSection, FooterSectionDto>().ReverseMap();
            CreateMap<FooterSection, CreateFooterSectionDto>().ReverseMap();
            CreateMap<FooterSection, GetFooterSectionDto>().ReverseMap();
            CreateMap<FooterSection, UpdateFooterSectionDto>().ReverseMap();

            CreateMap<FormField, FormFieldDto>().ReverseMap();
            CreateMap<FormField, CreateFormFieldDto>().ReverseMap();
            CreateMap<FormField, GetFormFieldDto>().ReverseMap();
            CreateMap<FormField, UpdateFormFieldDto>().ReverseMap();

            CreateMap<AboutSection, AboutSectionDto>().ReverseMap();
            CreateMap<AboutSection, GetAboutSectionDto>().ReverseMap();
            CreateMap<AboutSection, UpdateAboutSectionDto>().ReverseMap();
            CreateMap<AboutSection, CreateAboutSectionDto>().ReverseMap();

      
            CreateMap<ApiUserDto, ApiUser>().ReverseMap();

        }
    }
}
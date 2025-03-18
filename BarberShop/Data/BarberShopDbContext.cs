using BarberShop.Data.Configuration;
using BarberShop.Data.Entities;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using static NavbarConfiguration;



namespace BarberShop.Data
{

    public class BarberShopDbContext : IdentityDbContext<ApiUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantInfo _tenantInfo;

        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options, IHttpContextAccessor httpContextAccessor, ITenantInfo tenantInfo) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantInfo = tenantInfo;
        }

        // Existing DbSets
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<BarbersShop> BarberShops { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AboutSection> AboutSections { get; set; }
        public DbSet<BarberService> BarberServices { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContactInquiry> ContactInquiries { get; set; }
        public DbSet<CtaSection> CtaSections { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }

        public DbSet<BenefitSection> BenefitSections { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<NavbarAction> NavbarActions { get; set; }
        public DbSet<ContactFormSection> ContactFormSections { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<FooterSection> FooterSections { get; set; }
        public DbSet<StatsSection> StatsSections { get; set; }
        public DbSet<TeamSection> TeamSections { get; set; }
        public DbSet<StatItem> StatItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new BarberConfiguration());
            builder.ApplyConfiguration(new CtaSectionConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
            builder.ApplyConfiguration(new BarberServiceConfiguration());
            builder.ApplyConfiguration(new GalleryItemConfiguration());
            builder.ApplyConfiguration(new NewsletterSubscriberConfiguration());
            builder.ApplyConfiguration(new TestimonialConfiguration());
            builder.ApplyConfiguration(new FAQConfiguration());
            builder.ApplyConfiguration(new BlogPostConfiguration());
            builder.ApplyConfiguration(new ContactInquiryConfiguration());
            builder.ApplyConfiguration(new HeroSectionConfiguration());
            builder.ApplyConfiguration(new AboutSectionConfiguration());
            builder.ApplyConfiguration(new BenefitSectionConfiguration());
            builder.ApplyConfiguration(new NavbarConfiguration());
            builder.ApplyConfiguration(new NavbarActionConfiguration());
            builder.ApplyConfiguration(new ContactFormSectionConfiguration());
            builder.ApplyConfiguration(new FormFieldConfiguration());
            builder.ApplyConfiguration(new FooterSectionConfiguration());
            builder.ApplyConfiguration(new StatsSectionConfiguration());
            builder.ApplyConfiguration(new TeamSectionConfiguration());
            builder.ApplyConfiguration(new StatItemConfiguration());
            builder.ApplyConfiguration(new StatsSectionConfiguration());

            ApplyGlobalFilters<ITenant>(builder, "BarberShopId");
        }

        private void ApplyGlobalFilters<TInterface>(ModelBuilder builder, string tenantIdPropertyName) where TInterface : class
        {
            if (_tenantInfo.BarberShopId.HasValue)
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    if (typeof(ITenant).IsAssignableFrom(entityType.ClrType))
                    {
                        var parameter = Expression.Parameter(entityType.ClrType, "e");
                        var propertyMethod = typeof(EF).GetMethod("Property", BindingFlags.Static | BindingFlags.Public)
                            .MakeGenericMethod(typeof(Guid));
                        var barberShopIdProperty = Expression.Call(propertyMethod, parameter, Expression.Constant("BarberShopId"));
                        var barberShopIdValue = Expression.Constant(_tenantInfo.BarberShopId.Value, typeof(Guid));
                        var comparison = Expression.Equal(barberShopIdProperty, barberShopIdValue);
                        var lambda = Expression.Lambda(comparison, parameter);

                        builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    }
                }
            }
        }
    }
}
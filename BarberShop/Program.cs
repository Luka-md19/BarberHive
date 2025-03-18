using BarberShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.DataProtection;
using BarberShop.Middleware;
using BarberShop.Configuration;
using BarberShop.Contract;
using BarberShop.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using static BarberShop.Repository.NavbarRepository; 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BarberShopString");
builder.Services.AddDbContext<BarberShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarberShopString"))
           .LogTo(Console.WriteLine, LogLevel.Information));


// Add Data Protection services
builder.Services.AddDataProtection();

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("BarberShopAPI")
    .AddEntityFrameworkStores<BarberShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Barber API", Version = "v1" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });



    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});



builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<IBarberServiceRepository, BarberServiceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IContactInquiryRepository, ContactInquiryRepository>();
builder.Services.AddScoped<IFAQRepository, FAQRepository>();
builder.Services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();
builder.Services.AddScoped<INewsletterSubscriberRepository, NewsletterSubscriberRepository>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddScoped<IHeroSectionRepository, HeroSectionRepository>();
builder.Services.AddScoped<ICtaSectionRepository, CtaSectionRepository>();
builder.Services.AddScoped<IAboutSectionRepository, AboutSectionRepository>();
builder.Services.AddScoped<IBenefitSectionRepository, BenefitSectionRepository>();
builder.Services.AddScoped<IContactFormSectionRepository, ContactFormSectionRepository>();
builder.Services.AddScoped<IFooterSectionRepository, FooterSectionRepository>();
builder.Services.AddScoped<IFormFieldRepository, FormFieldRepository>();
builder.Services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();
builder.Services.AddScoped<INavbarRepository, NavbarRepository>();
builder.Services.AddScoped<INavbarActionRepository, NavbarActionRepository>();
builder.Services.AddScoped<IStatItemRepository, StatItemRepository>();
builder.Services.AddScoped<IStatsSectionRepository, StatsSectionRepository>();
builder.Services.AddScoped<ITeamSectionRepository, TeamSectionRepository>();
builder.Services.AddScoped<HomePageRepository>();
builder.Services.AddScoped<ServicesPageRepository>();
builder.Services.AddScoped<AboutUsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ITenantInfo, TenantInfo>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024;
    options.UseCaseSensitivePaths = true;
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Add the tenant resolution middleware here, after authentication and authorization
app.UseTenantResolution();

app.MapControllers();

app.Run();


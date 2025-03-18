using BarberShop.Contract;
using BarberShop.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagesResponses : ControllerBase // Ensure class name is correct here
    {
        private readonly HomePageRepository _homePageRepository;
        private readonly ServicesPageRepository _servicesPageRepository; // Added repository for services page
        private readonly AboutUsRepository _aboutUsRepository;

        public PagesResponses(HomePageRepository homePageRepository, ServicesPageRepository servicesPageRepository , AboutUsRepository aboutUsRepository) // Constructor name matches class name
        {
            _homePageRepository = homePageRepository;
            _servicesPageRepository = servicesPageRepository; // Initialize in constructor
            _aboutUsRepository = aboutUsRepository;
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHomePageData()
        {
            var homePageData = await _homePageRepository.GetHomePageDataAsync();
            return Ok(homePageData);
        }

        // New endpoint for the services page data
        [HttpGet("services")]
        public async Task<IActionResult> GetServicesPageData()
        {
            var servicesPageData = await _servicesPageRepository.GetServicesPageDataAsync();
            return Ok(servicesPageData);
        }

        [HttpGet("aboutUs")]
        public async Task<IActionResult> GetAboutPageData()
        {
            var aboutPageData = await _aboutUsRepository.GetAboutUsDataAsync();
            return Ok(aboutPageData);
        }
    }
}

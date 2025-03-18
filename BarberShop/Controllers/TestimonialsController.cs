using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.TestimonialDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Adjust authorization as needed
    public class TestimonialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialsController(IMapper mapper, ITestimonialRepository testimonialRepository)
        {
            _mapper = mapper;
            _testimonialRepository = testimonialRepository;
        }

        private Guid GetBarberShopId()
        {
            var barberShopIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;
            if (barberShopIdClaim == null)
            {
                throw new Exception("BarberShopId claim is missing.");
            }
            return Guid.Parse(barberShopIdClaim);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous] // If you want these endpoints to be accessible without authentication
        public async Task<ActionResult<IEnumerable<TestimonialDto>>> GetTestimonials()
        {
            var testimonials = await _testimonialRepository.GetAllAsync(GetBarberShopId());
            var testimonialDtos = _mapper.Map<IEnumerable<TestimonialDto>>(testimonials);

            if (!testimonialDtos.Any())
            {
                return NotFound("No testimonials found.");
            }
            return Ok(testimonialDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TestimonialDto>> GetTestimonial(int id)
        {
            var testimonial = await _testimonialRepository.GetAsync(id, GetBarberShopId());

            if (testimonial == null)
            {
                return NotFound($"No testimonial found with ID {id}.");
            }

            var testimonialDto = _mapper.Map<TestimonialDto>(testimonial);
            return Ok(testimonialDto);
        }

        [HttpPost]
        //[Authorize(Roles = "User")] // Uncomment if needed
        public async Task<ActionResult<TestimonialDto>> PostTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var testimonial = _mapper.Map<Testimonial>(createTestimonialDto);
            await _testimonialRepository.AddAsync(testimonial, GetBarberShopId());

            var testimonialDto = _mapper.Map<TestimonialDto>(testimonial);
            return CreatedAtAction(nameof(GetTestimonial), new { id = testimonialDto.Id }, testimonialDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestimonial(int id, UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonial = await _testimonialRepository.GetAsync(id, GetBarberShopId());
            if (testimonial == null)
            {
                return NotFound($"No testimonial found with ID {id}.");
            }

            _mapper.Map(updateTestimonialDto, testimonial);
            await _testimonialRepository.UpdateAsync(testimonial, GetBarberShopId());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            try
            {
                await _testimonialRepository.DeleteAsync(id, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No testimonial found with ID {id}.");
            }
        }
    }
}

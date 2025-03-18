using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.AboutSectionDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAboutSectionRepository _aboutSectionRepository;

        public AboutSectionsController(IMapper mapper, IAboutSectionRepository aboutSectionRepository)
        {
            _mapper = mapper;
            _aboutSectionRepository = aboutSectionRepository;
        }

        private Guid GetBarberShopId()
        {
            // Extracting BarberShopId from the JWT claim
            var barberShopIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;
            if (string.IsNullOrEmpty(barberShopIdClaim))
            {
                throw new Exception("BarberShopId claim is missing or invalid.");
            }
            return Guid.Parse(barberShopIdClaim);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<IEnumerable<GetAboutSectionDto>>> GetAboutSections()
        {
            var barberShopId = GetBarberShopId();
            var aboutSections = await _aboutSectionRepository.GetAllAsync<GetAboutSectionDto>(barberShopId);
            if (aboutSections == null || !aboutSections.Any())
            {
                return NotFound("No about sections found.");
            }
            return Ok(aboutSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<GetAboutSectionDto>> GetAboutSection(int id)
        {
            var barberShopId = GetBarberShopId();
            var aboutSectionDto = await _aboutSectionRepository.GetAsync<GetAboutSectionDto>(id, barberShopId);
            if (aboutSectionDto == null)
            {
                return NotFound($"No about section found with ID {id}.");
            }
            return Ok(aboutSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAboutSection(int id, UpdateAboutSectionDto updateAboutSectionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateAboutSectionDto.Id)
            {
                return BadRequest("Mismatched About Section ID.");
            }

            try
            {
                await _aboutSectionRepository.UpdateAsync(id, updateAboutSectionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No about section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the about section: {ex.Message}");
            }

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetAboutSectionDto>> PostAboutSection(CreateAboutSectionDto createAboutSectionDto)
        {
            var barberShopId = GetBarberShopId();
            var aboutSectionDto = await _aboutSectionRepository.AddAsync<CreateAboutSectionDto, GetAboutSectionDto>(createAboutSectionDto, barberShopId);
            return CreatedAtAction(nameof(GetAboutSection), new { id = aboutSectionDto.Id }, aboutSectionDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteAboutSection(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _aboutSectionRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No about section found with ID {id}.");
            }

            return NoContent();
        }
    }
}

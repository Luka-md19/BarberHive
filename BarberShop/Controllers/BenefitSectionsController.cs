using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.BenefitSectionDtos;
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
    public class BenefitSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBenefitSectionRepository _benefitSectionRepository;

        public BenefitSectionsController(IMapper mapper, IBenefitSectionRepository benefitSectionRepository)
        {
            _mapper = mapper;
            _benefitSectionRepository = benefitSectionRepository;
        }

        private Guid GetBarberShopId()
        {
            var barberShopIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;
            if (string.IsNullOrEmpty(barberShopIdClaim))
            {
                throw new Exception("BarberShopId claim is missing.");
            }
            return Guid.Parse(barberShopIdClaim);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<IEnumerable<GetBenefitDto>>> GetBenefitSections()
        {
            var barberShopId = GetBarberShopId();
            var benefitSections = await _benefitSectionRepository.GetAllAsync<GetBenefitDto>(barberShopId);
            if (benefitSections == null || !benefitSections.Any())
            {
                return NotFound("No benefit sections found.");
            }
            return Ok(benefitSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<GetBenefitDto>> GetBenefitSection(int id)
        {
            var barberShopId = GetBarberShopId();
            var benefitSectionDto = await _benefitSectionRepository.GetAsync<GetBenefitDto>(id, barberShopId);
            if (benefitSectionDto == null)
            {
                return NotFound($"No benefit section found with ID {id}.");
            }
            return Ok(benefitSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenefitSection(int id, UpdateBenefitDto updateBenefitSectionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateBenefitSectionDto.Id)
            {
                return BadRequest("Mismatched Benefit Section ID.");
            }

            try
            {
                await _benefitSectionRepository.UpdateAsync(id, updateBenefitSectionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No benefit section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the benefit section: {ex.Message}");
            }

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetBenefitDto>> PostBenefitSection(CreateBenefitDto createBenefitSectionDto)
        {
            var barberShopId = GetBarberShopId();
            var benefitSectionDto = await _benefitSectionRepository.AddAsync<CreateBenefitDto, GetBenefitDto>(createBenefitSectionDto, barberShopId);
            return CreatedAtAction(nameof(GetBenefitSection), new { id = benefitSectionDto.Id }, benefitSectionDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteBenefitSection(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _benefitSectionRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No benefit section found with ID {id}.");
            }

            return NoContent();
        }
    }
}

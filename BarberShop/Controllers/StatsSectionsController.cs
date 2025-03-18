using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.StatsDtos;
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
    public class StatsSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStatsSectionRepository _statsSectionRepository;

        public StatsSectionsController(IMapper mapper, IStatsSectionRepository statsSectionRepository)
        {
            _mapper = mapper;
            _statsSectionRepository = statsSectionRepository;
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
        [AllowAnonymous] // Adjust authorization as needed
        public async Task<ActionResult<IEnumerable<GetStatsSectionDto>>> GetStatsSections()
        {
            var barberShopId = GetBarberShopId();
            var statsSections = await _statsSectionRepository.GetAllAsync<GetStatsSectionDto>(barberShopId);
            if (statsSections == null || !statsSections.Any())
            {
                return NotFound("No stats sections found.");
            }
            return Ok(statsSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Adjust authorization as needed
        public async Task<ActionResult<StatsSectionDto>> GetStatsSection(int id)
        {
            var statsSectionDto = await _statsSectionRepository.GetStatsSectionAsync(id);
            if (statsSectionDto == null)
            {
                return NotFound($"No stats section found with ID {id}.");
            }
            return Ok(statsSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatsSection(int id, UpdateStatsSectionDto updateStatsSectionDto)
        {
            if (id != updateStatsSectionDto.Id)
            {
                return BadRequest("Mismatched Stats Section ID.");
            }

            try
            {
                await _statsSectionRepository.UpdateAsync(id, updateStatsSectionDto, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No stats section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the stats section: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetStatsSectionDto>> PostStatsSection(CreateStatsSectionDto createStatsSectionDto)
        {
            var statsSectionDto = await _statsSectionRepository.AddAsync<CreateStatsSectionDto, GetStatsSectionDto>(createStatsSectionDto, GetBarberShopId());
            return CreatedAtAction(nameof(GetStatsSection), new { id = statsSectionDto.Id }, statsSectionDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteStatsSection(int id)
        {
            try
            {
                await _statsSectionRepository.DeleteAsync(id, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No stats section found with ID {id}.");
            }
        }
    }
}

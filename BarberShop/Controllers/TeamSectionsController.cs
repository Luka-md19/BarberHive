using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.TeamDtos;
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
    [Authorize]
    public class TeamSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITeamSectionRepository _teamSectionRepository;

        public TeamSectionsController(IMapper mapper, ITeamSectionRepository teamSectionRepository)
        {
            _mapper = mapper;
            _teamSectionRepository = teamSectionRepository;
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
        public async Task<ActionResult<IEnumerable<GetTeamSectionDto>>> GetTeamSections()
        {
            var teamSections = await _teamSectionRepository.GetAllAsync<GetTeamSectionDto>(GetBarberShopId());
            if (teamSections == null || !teamSections.Any())
            {
                return NotFound("No team sections found.");
            }
            return Ok(teamSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Adjust authorization as needed
        public async Task<ActionResult<TeamSectionDto>> GetTeamSection(int id)
        {
            var teamSectionDto = await _teamSectionRepository.GetAsync<TeamSectionDto>(id, GetBarberShopId());
            if (teamSectionDto == null)
            {
                return NotFound($"No team section found with ID {id}.");
            }
            return Ok(teamSectionDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutTeamSection(int id, UpdateTeamSectionDto updateTeamSectionDto)
        {
            try
            {
                await _teamSectionRepository.UpdateAsync(id, updateTeamSectionDto, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No team section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the team section: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetTeamSectionDto>> PostTeamSection(CreateTeamSectionDto createTeamSectionDto)
        {
            var teamSectionDto = await _teamSectionRepository.AddAsync<CreateTeamSectionDto, GetTeamSectionDto>(createTeamSectionDto, GetBarberShopId());
            return CreatedAtAction(nameof(GetTeamSection), new { id = teamSectionDto.Id }, teamSectionDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTeamSection(int id)
        {
            try
            {
                await _teamSectionRepository.DeleteAsync(id, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No team section found with ID {id}.");
            }
        }
    }
}

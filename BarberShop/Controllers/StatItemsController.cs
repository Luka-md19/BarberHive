using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.StatItemDtos;
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
    public class StatItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStatItemRepository _statItemRepository;

        public StatItemsController(IMapper mapper, IStatItemRepository statItemRepository)
        {
            _mapper = mapper;
            _statItemRepository = statItemRepository;
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
        public async Task<ActionResult<IEnumerable<GetStatItemDto>>> GetStatItems()
        {
            var statItems = await _statItemRepository.GetAllAsync<GetStatItemDto>(GetBarberShopId());
            if (statItems == null || !statItems.Any())
            {
                return NotFound("No stat items found.");
            }
            return Ok(statItems);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetStatItemDto>> GetStatItem(int id)
        {
            var statItemDto = await _statItemRepository.GetAsync<GetStatItemDto>(id, GetBarberShopId());
            if (statItemDto == null)
            {
                return NotFound($"No stat item found with ID {id}.");
            }
            return Ok(statItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatItem(int id, UpdateStatItemDto updateStatItemDto)
        {
            if (id != updateStatItemDto.Id)
            {
                return BadRequest("Mismatched Stat Item ID.");
            }

            try
            {
                await _statItemRepository.UpdateAsync(id, updateStatItemDto, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No stat item found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the stat item: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetStatItemDto>> PostStatItem(CreateStatItemDto createStatItemDto)
        {
            var statItemDto = await _statItemRepository.AddAsync<CreateStatItemDto, GetStatItemDto>(createStatItemDto, GetBarberShopId());
            return CreatedAtAction(nameof(GetStatItem), new { id = statItemDto.Id }, statItemDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteStatItem(int id)
        {
            try
            {
                await _statItemRepository.DeleteAsync(id, GetBarberShopId());
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"No stat item found with ID {id}.");
            }
        }
    }
}

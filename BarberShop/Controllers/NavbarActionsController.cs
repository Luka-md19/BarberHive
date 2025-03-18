using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.NavbarDtos;
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
    public class NavbarActionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INavbarActionRepository _navbarActionRepository;

        public NavbarActionsController(IMapper mapper, INavbarActionRepository navbarActionRepository)
        {
            _mapper = mapper;
            _navbarActionRepository = navbarActionRepository;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NavbarActionDto>>> GetNavbarActions()
        {
            var barberShopId = GetBarberShopId();
            var navbarActions = await _navbarActionRepository.GetAllAsync<NavbarActionDto>(barberShopId);
            if (navbarActions == null || !navbarActions.Any())
            {
                return NotFound("No navbar actions found.");
            }
            return Ok(navbarActions);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<NavbarActionDto>> GetNavbarAction(int id)
        {
            var barberShopId = GetBarberShopId();
            var navbarActionDto = await _navbarActionRepository.GetAsync<NavbarActionDto>(id, barberShopId);
            if (navbarActionDto == null)
            {
                return NotFound($"No navbar action found with ID {id}.");
            }
            return Ok(navbarActionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavbarAction(int id, UpdateNavbarActionDto updateNavbarActionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateNavbarActionDto.Id)
            {
                return BadRequest("Mismatched Navbar Action ID.");
            }

            try
            {
                await _navbarActionRepository.UpdateAsync(id, updateNavbarActionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No navbar action found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the navbar action: " + ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<NavbarActionDto>> PostNavbarAction(CreateNavbarActionDto createNavbarActionDto)
        {
            var barberShopId = GetBarberShopId();
            var navbarActionDto = await _navbarActionRepository.AddAsync<CreateNavbarActionDto, NavbarActionDto>(createNavbarActionDto, barberShopId);
            return CreatedAtAction(nameof(GetNavbarAction), new { id = navbarActionDto.Id }, navbarActionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNavbarAction(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _navbarActionRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No navbar action found with ID {id}.");
            }

            return NoContent();
        }
    }
}

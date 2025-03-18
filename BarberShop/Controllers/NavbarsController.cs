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
    [Authorize] // Enforce authorization globally, specific endpoints can be made public using [AllowAnonymous]
    public class NavbarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INavbarRepository _navbarRepository;

        public NavbarsController(IMapper mapper, INavbarRepository navbarRepository)
        {
            _mapper = mapper;
            _navbarRepository = navbarRepository;
        }

        private Guid GetBarberShopId()
        {
            var barberShopIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;
            if (barberShopIdClaim == null)
            {
                throw new Exception("BarberShopId claim is missing."); // Updated for consistency
            }
            return Guid.Parse(barberShopIdClaim);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous] // Maintain this as public if desired
        public async Task<ActionResult<IEnumerable<GetNavbarDto>>> GetNavbars()
        {
            var barberShopId = GetBarberShopId();
            var navbars = await _navbarRepository.GetAllAsync<GetNavbarDto>(barberShopId);
            if (navbars == null || !navbars.Any())
            {
                return NotFound("No navbars found.");
            }
            return Ok(navbars);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Keep public access if needed
        public async Task<ActionResult<NavbarDto>> GetNavbar(int id)
        {
            var barberShopId = GetBarberShopId();
            var navbar = await _navbarRepository.GetNavbarAsync(id, barberShopId);

            if (navbar == null)
            {
                return NotFound($"No navbar found with ID {id}.");
            }

            var navbarDto = _mapper.Map<NavbarDto>(navbar);
            return Ok(navbarDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")] // Securing this endpoint
        public async Task<IActionResult> PutNavbar(int id, UpdateNavbarDto updateNavbarDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateNavbarDto.Id)
            {
                return BadRequest("Mismatched Navbar ID.");
            }

            try
            {
                await _navbarRepository.UpdateAsync(id, updateNavbarDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No navbar found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the navbar: " + ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")] // Ensuring that only administrators can add navbars
        public async Task<ActionResult<GetNavbarDto>> PostNavbar(CreateNavbarDto createNavbarDto)
        {
            var barberShopId = GetBarberShopId();
            var navbarDto = await _navbarRepository.AddAsync<CreateNavbarDto, GetNavbarDto>(createNavbarDto, barberShopId);
            return CreatedAtAction(nameof(GetNavbar), new { id = navbarDto.Id }, navbarDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")] // Secure delete operations to administrators
        public async Task<IActionResult> DeleteNavbar(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _navbarRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No navbar found with ID {id}.");
            }

            return NoContent();
        }
    }
}

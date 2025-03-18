using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.FooterSectionDtos;
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
    public class FooterSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFooterSectionRepository _footerSectionRepository;

        public FooterSectionsController(IMapper mapper, IFooterSectionRepository footerSectionRepository)
        {
            _mapper = mapper;
            _footerSectionRepository = footerSectionRepository;
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
        public async Task<ActionResult<IEnumerable<GetFooterSectionDto>>> GetFooterSections()
        {
            var barberShopId = GetBarberShopId();
            var footerSections = await _footerSectionRepository.GetAllAsync<GetFooterSectionDto>(barberShopId);
            if (footerSections == null || !footerSections.Any())
            {
                return NotFound("No footer sections found.");
            }
            return Ok(footerSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<GetFooterSectionDto>> GetFooterSection(int id)
        {
            var barberShopId = GetBarberShopId();
            var footerSectionDto = await _footerSectionRepository.GetAsync<GetFooterSectionDto>(id, barberShopId);
            if (footerSectionDto == null)
            {
                return NotFound($"No footer section found with ID {id}.");
            }
            return Ok(footerSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFooterSection(int id, UpdateFooterSectionDto updateFooterSectionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateFooterSectionDto.Id)
            {
                return BadRequest("Mismatched Footer Section ID.");
            }

            try
            {
                await _footerSectionRepository.UpdateAsync(id, updateFooterSectionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No footer section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the footer section: " + ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetFooterSectionDto>> PostFooterSection(CreateFooterSectionDto createFooterSectionDto)
        {
            var barberShopId = GetBarberShopId();
            var footerSectionDto = await _footerSectionRepository.AddAsync<CreateFooterSectionDto, GetFooterSectionDto>(createFooterSectionDto, barberShopId);
            return CreatedAtAction(nameof(GetFooterSection), new { id = footerSectionDto.Id }, footerSectionDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteFooterSection(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _footerSectionRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No footer section found with ID {id}.");
            }

            return NoContent();
        }
    }
}

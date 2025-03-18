using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.CtaSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Ensure this namespace is included for ILogger
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CtaSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICtaSectionRepository _ctaSectionRepository;
        private readonly ILogger<CtaSectionsController> _logger; // Specify the type parameter here

        public CtaSectionsController(IMapper mapper, ICtaSectionRepository ctaSectionRepository, ILogger<CtaSectionsController> logger) // Adjust the parameter type
        {
            _mapper = mapper;
            _ctaSectionRepository = ctaSectionRepository;
            _logger = logger;
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
        [AllowAnonymous] // Everyone can access this
        public async Task<ActionResult<IEnumerable<GetCtaSectionDto>>> GetCtaSections()
        {
            try
            {
                var barberShopId = GetBarberShopId();
                _logger.LogInformation($"Retrieving CTA sections for BarberShopId: {barberShopId}");
                var records = await _ctaSectionRepository.GetAllAsync<GetCtaSectionDto>(barberShopId);

                if (!records.Any())
                {
                    _logger.LogWarning($"No CTA sections found for BarberShopId: {barberShopId}");
                    return NotFound($"No CTA sections found for BarberShopId: {barberShopId}");
                }

                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving CTA sections");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        [AllowAnonymous] // Everyone can access this
        public async Task<ActionResult<CtaSectionDto>> GetCtaSection(int id)
        {
            var barberShopId = GetBarberShopId();
            var ctaSectionDto = await _ctaSectionRepository.GetAsync<CtaSectionDto>(id, barberShopId);
            if (ctaSectionDto == null)
            {
                return NotFound($"No CTA section found with ID {id}.");
            }
            return Ok(ctaSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCtaSection(int id, UpdateCtaSectionDto updateCtaSectionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateCtaSectionDto.Id)
            {
                return BadRequest("Mismatched CTA Section ID.");
            }

            try
            {
                await _ctaSectionRepository.UpdateAsync(id, updateCtaSectionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                if (!await CtaSectionExists(id, barberShopId))
                {
                    return NotFound($"No CTA section found with ID {id}.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CtaSectionDto>> PostCtaSection(CreateCtaSectionDto createCtaSectionDto)
        {
            var barberShopId = GetBarberShopId();
            var ctaSectionDto = await _ctaSectionRepository.AddAsync<CreateCtaSectionDto, GetCtaSectionDto>(createCtaSectionDto, barberShopId);
            return CreatedAtAction("GetCtaSection", new { id = ctaSectionDto.Id }, ctaSectionDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCtaSection(int id)
        {
            var barberShopId = GetBarberShopId();
            await _ctaSectionRepository.DeleteAsync(id, barberShopId);
            return NoContent();
        }

        private async Task<bool> CtaSectionExists(int id, Guid barberShopId)
        {
            return await _ctaSectionRepository.Exists(id, barberShopId);
        }
    }
}

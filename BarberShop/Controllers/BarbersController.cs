using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data;
using BarberShop.Exceptions;
using BarberShop.Models.BarberDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BarbersController : ControllerBase
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBarberRepository _barberRepository;
        private readonly ILogger<BarbersController> _logger;

        // Single constructor that DI can use
        public BarbersController(BarberShopDbContext context, IMapper mapper, IBarberRepository barberRepository, ILogger<BarbersController> logger)
        {
            this._context = context;
            _mapper = mapper;
            _barberRepository = barberRepository;
            _logger = logger;
        }


        private Guid GetBarberShopId()
        {
            var barberShopIdClaim = User.Claims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;
            if (string.IsNullOrEmpty(barberShopIdClaim))
            {
                var error = "BarberShopId claim is missing or invalid.";
                _logger.LogError(error);
                throw new Exception(error);
            }
            return Guid.Parse(barberShopIdClaim);
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<GetBarberDto>>> GetAllBarbersAsync()
        {
            
                var barberShopId = GetBarberShopId();

               

                var barbersDtos = await _barberRepository.GetAllAsync<GetBarberDto>(barberShopId);
                return Ok(barbersDtos);



        }



        [HttpGet("{barberShopId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<GetBarberDto>>> GetAllBarbers(Guid barberShopId)
        {
            var barbersDtos = await _barberRepository.GetAllAsync<GetBarberDto>(barberShopId);
            return Ok(barbersDtos);
        }

        [HttpGet("ById/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetBarberDto>> GetBarber(int id)
        {
            var barberShopId = GetBarberShopId();
            var barberDto = await _barberRepository.GetAsync<GetBarberDto>(id, barberShopId);
            if (barberDto == null)
            {
                _logger.LogWarning($"No barber found with ID: {id}.");
                return NotFound($"No barber found with ID {id}.");
            }

            return Ok(barberDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBarber(int id, UpdateBarberDto updateBarberDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateBarberDto.Id)
            {
                return BadRequest("Mismatched Barber ID.");
            }

            try
            {
                await _barberRepository.UpdateAsync(id, updateBarberDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No barber found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the barber: {ex.Message}");
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetBarberDto>> PostBarber(CreateBarberDto createBarberDto)
        {
            var barberShopId = GetBarberShopId();
            var barberDto = await _barberRepository.AddAsync<CreateBarberDto, GetBarberDto>(createBarberDto, barberShopId);
            return CreatedAtAction(nameof(GetBarber), new { id = barberDto.Id }, barberDto);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBarber(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _barberRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No barber found with ID {id}.");
            }

            return NoContent();
        }
    }
}
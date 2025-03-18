using AutoMapper;
using BarberShop.Contract;
using BarberShop.Models.BarberServicesDtos;
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
    public class BarberServicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBarberServiceRepository _barberServiceRepository;

        public BarberServicesController(IMapper mapper, IBarberServiceRepository barberServiceRepository)
        {
            _mapper = mapper;
            _barberServiceRepository = barberServiceRepository;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetBarberServicesDto>>> GetBarberServices()
        {
            var barberShopId = GetBarberShopId();
            var barberServices = await _barberServiceRepository.GetAllAsync(barberShopId); // Assuming GetAllAsync accepts BarberShopId now
            var barberServiceDtos = _mapper.Map<IEnumerable<GetBarberServicesDto>>(barberServices);

            if (!barberServiceDtos.Any())
            {
                return NotFound("No barber services found.");
            }
            return Ok(barberServiceDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetBarberServicesDto>> GetBarberService(int id)
        {
            var barberShopId = GetBarberShopId();
            var barberService = await _barberServiceRepository.GetAsync(id, barberShopId); // Assuming GetAsync accepts BarberShopId now

            if (barberService == null)
            {
                return NotFound();
            }

            var barberServiceDto = _mapper.Map<GetBarberServicesDto>(barberService);
            return Ok(barberServiceDto);
        }

        [HttpPost]
        public async Task<ActionResult<BarberServicesDto>> PostBarberService(CreateBarberServicesDto createBarberServiceDto)
        {
            var barberShopId = GetBarberShopId();
            var barberService = _mapper.Map<BarberService>(createBarberServiceDto);
            await _barberServiceRepository.AddAsync(barberService, barberShopId); // Assuming AddAsync now accepts BarberShopId

            var barberServiceDto = _mapper.Map<BarberServicesDto>(barberService);
            return CreatedAtAction("GetBarberService", new { id = barberServiceDto.Id }, barberServiceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarberService(int id, UpdateBarberServicesDto updateBarberServiceDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateBarberServiceDto.Id)
            {
                return BadRequest("Mismatched BarberService ID.");
            }

            var barberService = await _barberServiceRepository.GetAsync(id, barberShopId); // Assuming GetAsync now accepts BarberShopId
            if (barberService == null)
            {
                return NotFound();
            }

            _mapper.Map(updateBarberServiceDto, barberService);
            await _barberServiceRepository.UpdateAsync(barberService, barberShopId); // Assuming UpdateAsync now accepts BarberShopId

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarberService(int id)
        {
            var barberShopId = GetBarberShopId();
            await _barberServiceRepository.DeleteAsync(id, barberShopId); // Assuming DeleteAsync now accepts BarberShopId
            return NoContent();
        }
    }
}

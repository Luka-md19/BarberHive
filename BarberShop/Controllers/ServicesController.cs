using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data.Entities;
using BarberShop.Exceptions;
using BarberShop.Models.ServiceDtos;
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
    public class ServicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IMapper mapper, IServiceRepository serviceRepository)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
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
        [AllowAnonymous] // Everyone can access this
        public async Task<ActionResult<IEnumerable<GetServiceDto>>> GetAll()
        {
            var barberShopId = GetBarberShopId();
            var records = await _serviceRepository.GetAllAsync<GetServiceDto>(barberShopId);
            return Ok(records);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Everyone can access this
        public async Task<ActionResult<ServiceDto>> Get(int id)
        {
            var barberShopId = GetBarberShopId();
            var serviceDto = await _serviceRepository.GetAsync<ServiceDto>(id, barberShopId);
            if (serviceDto == null)
            {
                return NotFound($"No service found with ID {id}.");
            }
            return Ok(serviceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateServiceDto updateServiceDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateServiceDto.Id)
            {
                return BadRequest("Mismatched Service ID.");
            }

            try
            {
                await _serviceRepository.UpdateAsync(id, updateServiceDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No service found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the service: {ex.Message}");
            }
            return NoContent();
        }

        [HttpPost]
        // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Service>> Create(CreateServiceDto createServiceDto)
        {
            var barberShopId = GetBarberShopId();
            var serviceDto = await _serviceRepository.AddAsync<CreateServiceDto, GetServiceDto>(createServiceDto, barberShopId);
            return CreatedAtAction(nameof(Get), new { id = serviceDto.Id }, serviceDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _serviceRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No service found with ID {id}.");
            }
            return NoContent();
        }

        private async Task<bool> ServiceExists(int id)
        {
            var barberShopId = GetBarberShopId();
            return await _serviceRepository.Exists(id, barberShopId);
        }
    }
}

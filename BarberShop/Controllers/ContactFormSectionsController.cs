using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.ContactSectionDtos;
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
    public class ContactFormSectionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactFormSectionRepository _contactFormSectionRepository;

        public ContactFormSectionsController(IMapper mapper, IContactFormSectionRepository contactFormSectionRepository)
        {
            _mapper = mapper;
            _contactFormSectionRepository = contactFormSectionRepository;
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
        public async Task<ActionResult<IEnumerable<GetContactDto>>> GetContactFormSections()
        {
            var barberShopId = GetBarberShopId();
            var contactFormSections = await _contactFormSectionRepository.GetAllAsync<GetContactDto>(barberShopId);
            if (contactFormSections == null || !contactFormSections.Any())
            {
                return NotFound("No contact form sections found.");
            }
            return Ok(contactFormSections);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<GetContactDto>> GetContactFormSection(int id)
        {
            var barberShopId = GetBarberShopId();
            var contactFormSectionDto = await _contactFormSectionRepository.GetAsync<GetContactDto>(id, barberShopId);
            if (contactFormSectionDto == null)
            {
                return NotFound($"No contact form section found with ID {id}.");
            }
            return Ok(contactFormSectionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactFormSection(int id, UpdateContactDto updateContactFormSectionDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateContactFormSectionDto.Id)
            {
                return BadRequest("Mismatched Contact Form Section ID.");
            }

            try
            {
                await _contactFormSectionRepository.UpdateAsync(id, updateContactFormSectionDto, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No contact form section found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the contact form section: " + ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetContactDto>> PostContactFormSection(CreateContactDto createContactFormSectionDto)
        {
            var barberShopId = GetBarberShopId();
            var contactFormSectionDto = await _contactFormSectionRepository.AddAsync<CreateContactDto, GetContactDto>(createContactFormSectionDto, barberShopId);
            return CreatedAtAction(nameof(GetContactFormSection), new { id = contactFormSectionDto.Id }, contactFormSectionDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteContactFormSection(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _contactFormSectionRepository.DeleteAsync(id, barberShopId);
            }
            catch (NotFoundException)
            {
                return NotFound($"No contact form section found with ID {id}.");
            }

            return NoContent();
        }
    }
}

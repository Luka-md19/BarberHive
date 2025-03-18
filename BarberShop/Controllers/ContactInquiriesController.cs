using AutoMapper;
using BarberShop.Contract;
using BarberShop.Models.ContactInquiryDtos;
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
    public class ContactInquiriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactInquiryRepository _contactInquiryRepository;

        public ContactInquiriesController(IMapper mapper, IContactInquiryRepository contactInquiryRepository)
        {
            _mapper = mapper;
            _contactInquiryRepository = contactInquiryRepository;
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
        public async Task<ActionResult<IEnumerable<GetContactInquiryDto>>> GetContactInquiries()
        {
            var barberShopId = GetBarberShopId();
            var contactInquiries = await _contactInquiryRepository.GetAllAsync(barberShopId);
            var contactInquiryDtos = _mapper.Map<IEnumerable<GetContactInquiryDto>>(contactInquiries);

            if (!contactInquiryDtos.Any())
            {
                return NotFound("No contact inquiries found.");
            }
            return Ok(contactInquiryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactInquiryDto>> GetContactInquiry(int id)
        {
            var barberShopId = GetBarberShopId();
            var contactInquiry = await _contactInquiryRepository.GetAsync(id, barberShopId);

            if (contactInquiry == null)
            {
                return NotFound();
            }

            var contactInquiryDto = _mapper.Map<GetContactInquiryDto>(contactInquiry);
            return Ok(contactInquiryDto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactInquiryDto>> PostContactInquiry(CreateContactInquiryDto createContactInquiryDto)
        {
            var barberShopId = GetBarberShopId();
            var contactInquiry = _mapper.Map<ContactInquiry>(createContactInquiryDto);
            await _contactInquiryRepository.AddAsync(contactInquiry, barberShopId);

            var contactInquiryDto = _mapper.Map<ContactInquiryDto>(contactInquiry);
            return CreatedAtAction("GetContactInquiry", new { id = contactInquiryDto.Id }, contactInquiryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactInquiry(int id, UpdateContactInquiryDto updateContactInquiryDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateContactInquiryDto.Id)
            {
                return BadRequest("Mismatched Contact Inquiry ID.");
            }

            var contactInquiry = await _contactInquiryRepository.GetAsync(id, barberShopId);
            if (contactInquiry == null)
            {
                return NotFound();
            }

            _mapper.Map(updateContactInquiryDto, contactInquiry);
            await _contactInquiryRepository.UpdateAsync(contactInquiry, barberShopId);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInquiry(int id)
        {
            var barberShopId = GetBarberShopId();
            var contactInquiry = await _contactInquiryRepository.GetAsync(id, barberShopId);
            if (contactInquiry == null)
            {
                return NotFound($"No contact inquiry found with ID {id}.");
            }

            await _contactInquiryRepository.DeleteAsync(id, barberShopId);
            return NoContent();
        }
    }
}

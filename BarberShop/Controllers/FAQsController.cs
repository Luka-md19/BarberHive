using AutoMapper;
using BarberShop.Contract;
using BarberShop.Models.FaqDtos;
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
    public class FAQsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFAQRepository _faqRepository;

        public FAQsController(IMapper mapper, IFAQRepository faqRepository)
        {
            _mapper = mapper;
            _faqRepository = faqRepository;
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
        public async Task<ActionResult<IEnumerable<GetFaqDto>>> GetFAQs()
        {
            var barberShopId = GetBarberShopId();
            var faqs = await _faqRepository.GetAllAsync(barberShopId); // Assume GetAllAsync now requires BarberShopId
            var faqDtos = _mapper.Map<IEnumerable<GetFaqDto>>(faqs);

            if (!faqDtos.Any())
            {
                return NotFound("No FAQs found.");
            }
            return Ok(faqDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetFaqDto>> GetFAQ(int id)
        {
            var barberShopId = GetBarberShopId();
            var faq = await _faqRepository.GetAsync(id, barberShopId); // Assume GetAsync now requires BarberShopId

            if (faq == null)
            {
                return NotFound();
            }

            var faqDto = _mapper.Map<GetFaqDto>(faq);
            return Ok(faqDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment and adjust as necessary
        public async Task<ActionResult<FaqDto>> PostFAQ(CreateFaqDto createFaqDto)
        {
            var barberShopId = GetBarberShopId();
            var faq = _mapper.Map<FAQ>(createFaqDto);
            await _faqRepository.AddAsync(faq, barberShopId); // Assume AddAsync now requires BarberShopId

            var faqDto = _mapper.Map<FaqDto>(faq);
            return CreatedAtAction(nameof(GetFAQ), new { id = faqDto.Id }, faqDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFAQ(int id, UpdateFaqDto updateFaqDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateFaqDto.Id)
            {
                return BadRequest("Mismatched FAQ ID.");
            }

            var faq = await _faqRepository.GetAsync(id, barberShopId); // Assume GetAsync now requires BarberShopId
            if (faq == null)
            {
                return NotFound();
            }

            _mapper.Map(updateFaqDto, faq);
            await _faqRepository.UpdateAsync(faq, barberShopId); // Assume UpdateAsync now requires BarberShopId

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment and adjust as necessary
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var barberShopId = GetBarberShopId();
            await _faqRepository.DeleteAsync(id, barberShopId); // Assume DeleteAsync now requires BarberShopId
            return NoContent();
        }
    }
}

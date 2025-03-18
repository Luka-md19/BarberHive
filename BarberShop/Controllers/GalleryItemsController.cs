using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.GalleryItemDtos;
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
    public class GalleryItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGalleryItemRepository _galleryItemRepository;

        public GalleryItemsController(IMapper mapper, IGalleryItemRepository galleryItemRepository)
        {
            _mapper = mapper;
            _galleryItemRepository = galleryItemRepository;
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
        public async Task<ActionResult<IEnumerable<GalleryItemDto>>> GetGalleryItems()
        {
            var barberShopId = GetBarberShopId();
            var galleryItems = await _galleryItemRepository.GetAllAsync(barberShopId); // Assuming GetAllAsync now accepts BarberShopId
            var galleryItemDtos = _mapper.Map<IEnumerable<GalleryItemDto>>(galleryItems);

            if (!galleryItemDtos.Any())
            {
                return NotFound("No gallery items found.");
            }
            return Ok(galleryItemDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GalleryItemDto>> GetGalleryItem(int id)
        {
            var barberShopId = GetBarberShopId();
            var galleryItem = await _galleryItemRepository.GetAsync(id, barberShopId); // Assuming GetAsync now requires BarberShopId

            if (galleryItem == null)
            {
                return NotFound();
            }

            var galleryItemDto = _mapper.Map<GalleryItemDto>(galleryItem);
            return Ok(galleryItemDto);
        }

        [HttpPost]
        public async Task<ActionResult<GalleryItemDto>> PostGalleryItem(CreateGalleryItemDto createGalleryItemDto)
        {
            var barberShopId = GetBarberShopId();
            var galleryItemDto = await _galleryItemRepository.AddAsync<CreateGalleryItemDto, GalleryItemDto>(createGalleryItemDto, barberShopId); // Assuming AddAsync now accepts BarberShopId
            return CreatedAtAction(nameof(GetGalleryItem), new { id = galleryItemDto.Id }, galleryItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGalleryItem(int id, UpdateGalleryItemDto updateGalleryItemDto)
        {
            var barberShopId = GetBarberShopId();
            if (id != updateGalleryItemDto.Id)
            {
                return BadRequest("Mismatched Gallery Item ID.");
            }

            try
            {
                await _galleryItemRepository.UpdateAsync(id, updateGalleryItemDto, barberShopId); // Assuming UpdateAsync now accepts BarberShopId
            }
            catch (NotFoundException)
            {
                return NotFound($"No gallery item found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the gallery item: " + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalleryItem(int id)
        {
            var barberShopId = GetBarberShopId();
            try
            {
                await _galleryItemRepository.DeleteAsync(id, barberShopId); // Assuming DeleteAsync now accepts BarberShopId
            }
            catch (NotFoundException)
            {
                return NotFound($"No gallery item found with ID {id}.");
            }

            return NoContent();
        }
    }
}

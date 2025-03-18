using AutoMapper;
using BarberShop.Contract;
using BarberShop.Data.Entities;
using BarberShop.Exceptions;
using BarberShop.Models.HeroSection;
using BarberShop.Models.HeroSectionDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class HeroSectionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IHeroSectionRepository _heroSectionRepository;

    public HeroSectionsController(IMapper mapper, IHeroSectionRepository heroSectionRepository)
    {
        _mapper = mapper;
        _heroSectionRepository = heroSectionRepository;
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
    public async Task<ActionResult<IEnumerable<GetHeroSectionDto>>> GetHeroSections()
    {
        var barberShopId = GetBarberShopId();
        var records = await _heroSectionRepository.GetAllAsync<GetHeroSectionDto>(barberShopId);
        return Ok(records);
    }

    [HttpGet("{id}")]
    [AllowAnonymous] // Everyone can access this
    public async Task<ActionResult<HeroSectionDto>> GetHeroSection(int id)
    {
        var barberShopId = GetBarberShopId();
        var heroSectionDto = await _heroSectionRepository.GetAsync<HeroSectionDto>(id, barberShopId);
        if (heroSectionDto == null)
        {
            return NotFound();
        }
        return Ok(heroSectionDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHeroSection(int id, UpdateHeroSectionDto updateHeroSectionDto)
    {
        var barberShopId = GetBarberShopId();
        if (id != updateHeroSectionDto.Id)
        {
            return BadRequest("Mismatched Hero Section ID.");
        }

        try
        {
            await _heroSectionRepository.UpdateAsync(id, updateHeroSectionDto, barberShopId);
        }
        catch (NotFoundException)
        {
            if (!await HeroSectionExists(id))
            {
                return NotFound();
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
    public async Task<ActionResult<HeroSection>> PostHeroSection(CreateHeroSectionDto createHeroSectionDto)
    {
        var barberShopId = GetBarberShopId();
        var heroSectionDto = await _heroSectionRepository.AddAsync<CreateHeroSectionDto, GetHeroSectionDto>(createHeroSectionDto, barberShopId);
        return CreatedAtAction("GetHeroSection", new { id = heroSectionDto.Id }, heroSectionDto);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteHeroSection(int id)
    {
        var barberShopId = GetBarberShopId();
        await _heroSectionRepository.DeleteAsync(id, barberShopId);
        return NoContent();
    }

    private async Task<bool> HeroSectionExists(int id)
    {
        var barberShopId = GetBarberShopId();
        return await _heroSectionRepository.Exists(id, barberShopId);
    }
}

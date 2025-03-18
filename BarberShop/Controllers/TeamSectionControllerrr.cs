using AutoMapper;
using BarberShop.Contract;
using BarberShop.Models.TeamDtos;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TeamSectionController : ControllerBase
{
    private readonly ITeamSectionRepository _teamSectionRepository;
    private readonly IMapper _mapper;

    public TeamSectionController(ITeamSectionRepository teamSectionRepository, IMapper mapper)
    {
        _teamSectionRepository = teamSectionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamSectionDto>>> GetAllTeamSections()
    {
        Guid barberShopId = new Guid(HttpContext.Items["BarberShopId"].ToString());
        var teamSections = await _teamSectionRepository.GetAllTeamSectionsAsync(barberShopId);
        return Ok(_mapper.Map<IEnumerable<TeamSectionDto>>(teamSections));
    }
}

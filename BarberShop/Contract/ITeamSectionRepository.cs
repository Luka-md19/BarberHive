using BarberShop.Models.TeamDtos;

namespace BarberShop.Contract
{
    public interface ITeamSectionRepository : IGenericRepository<TeamSection>
    {
        Task<TeamSectionDto> GetTeamSectionAsync(int id);
        Task<List<TeamSection>> GetAllTeamSectionsAsync(Guid barberShopId);
    }
}

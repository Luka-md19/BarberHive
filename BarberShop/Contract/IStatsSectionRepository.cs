using BarberShop.Models.StatsDtos;

namespace BarberShop.Contract
{
    public interface IStatsSectionRepository : IGenericRepository<StatsSection>
    {
         Task<StatsSectionDto> GetStatsSectionAsync(int id);
    }
}

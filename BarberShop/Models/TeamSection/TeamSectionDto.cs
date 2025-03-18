using BarberShop.Models.BarberDtos;
using Newtonsoft.Json;

namespace BarberShop.Models.TeamDtos
{
    public class TeamSectionDto : BaseTeamSectionDto
    {
        
        public int Id { get; set; }
       
        public virtual ICollection<BarberDto> TeamMembers { get; set; }
    }
}

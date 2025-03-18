using BarberShop.Models.NavbarAction;

namespace BarberShop.Models.NavbarDtos
{
    public class NavbarDto : BaseNavbarDto
    {
        public int Id { get; set; }
        public List<NavbarActionDto> Actions { get; set; }
    }
}
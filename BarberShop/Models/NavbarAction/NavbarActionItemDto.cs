namespace BarberShop.Models.NavbarAction
{
    public class NavbarActionItemDto
    {
        public List<string> Sections { get; set; }
        public List<NavbarActionItemDto> Actions { get; set; }
    }
}

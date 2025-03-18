namespace BarberShop.Models.User
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string BarberShopId { get; set; }
    }
}

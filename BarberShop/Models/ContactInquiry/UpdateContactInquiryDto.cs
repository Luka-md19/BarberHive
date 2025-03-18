namespace BarberShop.Models.ContactInquiryDtos
{
    public class UpdateContactInquiryDto : BaseContactInquiryDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }
    }
}

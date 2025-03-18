using BarberShop.Models.ServiceDtos;
using BarberShop.Models.User;

namespace BarberShop.Models.TestimonialDtos
{
    public class TestimonialDto : BaseTestimonialDto
    {

        public int Id { get; set; }
        public DateTime DatePosted { get; set; }
        // Assuming you'd want to show customer and service details in the testimonial DTO
        //public ApiUserDto Customer { get; set; }
        //public ServiceDto Service { get; set; }
    }
}

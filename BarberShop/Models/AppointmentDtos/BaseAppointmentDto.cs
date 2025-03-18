using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models.AppointmentDtos
{
    public abstract class BaseAppointmentDto
    {
        public string CalendlyEventId { get; set; }
        public string CustomerId { get; set; }
        public int? ServiceId { get; set; }
        public int? BarberId { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string Status { get; set; }

        public Guid BarberShopId { get; set; }
    }
}

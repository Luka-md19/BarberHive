using BarberShop.Data;
using BarberShop.Data.Configuration;
using BarberShop.Repository;
using System;
using System.ComponentModel.DataAnnotations;

public class Appointment : ITenant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CalendlyEventId { get; set; } // Unique identifier for the Calendly event

    [Required]
    public string CustomerId { get; set; }
    public virtual ApiUser Customer { get; set; } // Links to registered users, if applicable

    public int? ServiceId { get; set; } // Maps to specific services offered
    public virtual Service Service { get; set; }

    public int? BarberId { get; set; } // Optional: Links to a specific barber for the appointment
    public virtual Barber Barber { get; set; }

    [Required]
    public DateTime AppointmentStart { get; set; } // Start time

    [Required]
    public DateTime AppointmentEnd { get; set; } // End time

    [MaxLength(50)]
    public string Status { get; set; } // E.g., Scheduled, Completed, Cancelled

    [Required]
    public Guid BarberShopId { get; set; }
}

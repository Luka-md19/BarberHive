using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;

    public BookingController(ILogger<BookingController> logger)
    {
        _logger = logger;
    }

    // Define BookingController actions here
}

[ApiController]
[Route("api/[controller]")]
public class RedirectController : ControllerBase
{
    [HttpGet("book-appointment")]
    public IActionResult BookAppointment()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect("https://calendly.com/malekh387/barbershop");
        }
        else
        {
            return Unauthorized("You must be logged in to book an appointment.");
        }
    }
}

using Microsoft.Extensions.Configuration;

public class CalendlyService
{
    private readonly string _calendlyToken;

    public CalendlyService(IConfiguration configuration)
    {
        _calendlyToken = configuration["CalendlyToken"];
    }
}

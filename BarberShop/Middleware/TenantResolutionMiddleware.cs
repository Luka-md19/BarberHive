using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantInfo tenantInfo)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userClaims = context.User.Claims;
            var barberShopIdClaim = userClaims.FirstOrDefault(c => c.Type == "BarberShopId")?.Value;

            if (!string.IsNullOrEmpty(barberShopIdClaim) && Guid.TryParse(barberShopIdClaim, out var barberShopId))
            {
                tenantInfo.BarberShopId = barberShopId;
                context.Items["BarberShopId"] = barberShopIdClaim;
            }
        }

        await _next(context);
    }
}

public static class TenantResolutionMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantResolution(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantResolutionMiddleware>();
    }
}

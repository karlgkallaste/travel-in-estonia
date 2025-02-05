using Microsoft.Extensions.DependencyInjection;
using TravelInEstonia.Services.Services;

namespace TravelInEstonia.Services;

public static class ServicesServiceCollection
{
    public static IServiceCollection RegisterServicesServices(this IServiceCollection services)
    {
        services.AddScoped<IBusScheduleService, BusScheduleService>();
        return services;
    }
}
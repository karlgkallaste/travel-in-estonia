using Microsoft.Extensions.DependencyInjection;
using TravelInEstonia.Domain.Features.Schedules.Commands;

namespace TravelInEstonia.Domain;

public static class DomainServiceCollection
{
    public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateScheduleCommand).Assembly));
        return services;
    }
}
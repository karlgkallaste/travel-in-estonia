using System.Collections.Immutable;

namespace TravelInEstonia.Domain.Features.Schedules.Events;

public class ScheduleCreated(Guid id, DateTimeOffset validUntil, ImmutableList<Route> routes)
{
    public Guid Id { get; private set; } = id;
    public DateTimeOffset ValidUntil { get; private set; } = validUntil;
    public ImmutableList<Route> Routes { get; set; } = routes;
}
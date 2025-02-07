using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TravelInEstonia.Domain.Features.Schedules.Events;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Schedules;

public class Schedule : IWithId
{
    public Guid Id { get; private set; }
    public DateTimeOffset ValidUntil { get; private set; }
    public ImmutableList<Route> Routes { get; private set; } = ImmutableList<Route>.Empty;

    protected Schedule() { }

    [JsonConstructor]
    public Schedule(Guid id, DateTimeOffset validUntil, ImmutableList<Route> routes)
    {
        Id = id;
        ValidUntil = validUntil;
        Routes = routes;
    }

    private void Apply(ScheduleCreated @event)
    {
        Id = @event.Id;
        ValidUntil = @event.ValidUntil;
        Routes = @event.Routes;
    }
}
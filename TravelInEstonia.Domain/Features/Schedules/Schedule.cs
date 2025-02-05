using System.Collections.Immutable;
using TravelInEstonia.Domain.Features.Schedules.Events;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Schedules;

public class Schedule : IWithId
{
    public Guid Id { get; private set; }
    public DateTimeOffset ValidUntil { get; private set; }
    public ImmutableList<Route> Routes { get; private set; } = ImmutableList<Route>.Empty;

    private void Apply(ScheduleCreated @event)
    {
        Id = @event.Id;
        ValidUntil = @event.ValidUntil;
        Routes = @event.Routes;
    }
}
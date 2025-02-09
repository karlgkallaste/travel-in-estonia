using System.Collections.Immutable;
using TravelInEstonia.Domain.Features.Reservations.Events;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Reservations;

public class Reservation : IWithId
{
    public Guid Id { get; private set; }
    public Guid ScheduleId { get; private set; }
    public ImmutableList<ReservedRoute> Routes { get; set; } = ImmutableList<ReservedRoute>.Empty;

    private void Apply(ReservationCreated @event)
    {
        Id = @event.Id;
        ScheduleId = @event.ScheduleId;
        Routes = @event.Routes;
    }
}
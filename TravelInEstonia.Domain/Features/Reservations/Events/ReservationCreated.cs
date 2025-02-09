using System.Collections.Immutable;

namespace TravelInEstonia.Domain.Features.Reservations.Events;

public class ReservationCreated(Guid id, Guid scheduleId, string firstName, string lastName, ImmutableList<ReservedRoute> routes)
{
    public Guid Id { get; private set; } = id;
    public Guid ScheduleId { get; private set; } = scheduleId;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public ImmutableList<ReservedRoute> Routes { get; private set; } = routes;
}
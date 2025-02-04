using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Schedules;

public class Schedule : IWithId
{
    public Guid Id { get; set; }
}
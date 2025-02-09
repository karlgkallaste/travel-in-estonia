using TravelInEstonia.Domain.Features.Schedules;

namespace TravelInEstonia.Domain.Features.Reservations;

public record ReservedRoute(Guid Id, Location From, Location To, DateTimeOffset DepartAt, DateTimeOffset ArriveBy, Company Company, double Price, double Distance);
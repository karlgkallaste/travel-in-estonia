using System.Collections.Immutable;

namespace TravelInEstonia.Domain.Features.Schedules;

public record Route(Guid Id, Location From, Location To, double Distance, ImmutableList<Provider> Providers);
public record Location(Guid Id, string Name);

public record Provider(Guid Id, double Price, DateTimeOffset DepartAt, DateTimeOffset ArriveBy, Company Company);
public record Company(Guid Id, string Name);
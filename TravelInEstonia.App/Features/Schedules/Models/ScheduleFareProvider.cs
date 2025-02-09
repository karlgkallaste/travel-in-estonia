using System.Collections.Immutable;
using TravelInEstonia.Domain.Features.Reservations;
using TravelInEstonia.Domain.Features.Schedules;
using Route = TravelInEstonia.Domain.Features.Schedules.Route;

namespace TravelInEstonia.App.Features.Schedules.Models;

public interface IScheduleFareProvider
{
    ImmutableList<FareModel> Provide(Schedule schedule, ScheduleFiltersModel filtersModel);
}

public class ScheduleFareProvider : IScheduleFareProvider
{
    public ImmutableList<FareModel> Provide(Schedule schedule, ScheduleFiltersModel filtersModel)
    {
        // Step 1: Validate input
        if (schedule == null) throw new ArgumentNullException(nameof(schedule));

        // Step 2: Build the graph (Adjacency List)
        var graph = new Dictionary<string, List<Route>>();
        foreach (var route in schedule.Routes)
        {
            if (!graph.ContainsKey(route.From.Name))
                graph[route.From.Name] = new List<Route>();

            graph[route.From.Name].Add(route);
        }

        // Step 3: Find all paths from `fromId` to `toId` using DFS
        var allPaths = new List<List<Route>>();

        // Helper method to recursively find paths from start to end
        void FindPaths(string current, string destination, DateTimeOffset departingAfter, List<Route> currentPath, HashSet<string> visited)
        {
            if (visited.Contains(current)) return; // Avoid cycles

            visited.Add(current);

            if (graph.ContainsKey(current))
            {
                foreach (var route in graph[current])
                {
                    var newPath = new List<Route>(currentPath) { route };

                    // If we reached the destination and departure date is valid
                    if (route.To.Name == destination
                        && route.Providers.Any(x => x.DepartAt.Date > departingAfter.Date))
                    {
                        allPaths.Add(newPath); // Found a valid path
                    }
                    else
                    {
                        FindPaths(route.To.Name, destination, departingAfter, newPath, visited); // Recursively explore next routes
                    }
                }
            }

            visited.Remove(current);
        }

        // Step 4: Find all paths starting from `From` to `To`
        var visited = new HashSet<string>();
        FindPaths(filtersModel.From, filtersModel.To, filtersModel.DepartureDate, new List<Route>(), visited);

        // Step 5: Transform the paths into FareModel
        return allPaths
            .SelectMany(path =>
                    path.SelectMany(route => route.Providers.Select(provider => (route, provider))) // Flatten routes and providers
            )
            .GroupBy(entry =>
            {
                // Group by company, from, to, and departure time to ensure uniqueness of each route
                return new
                {
                    FromName = entry.route.From.Name,
                    ToName = entry.route.To.Name,
                    DepartureTime = entry.provider.DepartAt,
                    entry.provider.Company.Id
                };
            })
            .Select(providerGroup =>
            {

                var routeModels = providerGroup.Select(entry => new RouteModel(
                    entry.route.Id,
                    entry.provider.DepartAt,
                    entry.provider.ArriveBy,
                    new LocationModel(entry.route.From.Id, entry.route.From.Name),
                    new LocationModel(entry.route.To.Id, entry.route.To.Name),
                    entry.route.Distance,
                    entry.provider.Price,
                    new CompanyModel(entry.provider.Company.Id, entry.provider.Company.Name)
                )).ToArray();

                return new FareModel(
                    schedule.Id,
                    routeModels,
                    routeModels.Sum(r => r.Distance),
                    providerGroup.Sum(entry => entry.provider.Price)
                );
            }).ToImmutableList();
    }
}

public record FareModel(Guid ScheduleId, RouteModel[] Routes, double TotalDistance, double TotalPrice);
public record CompanyModel(Guid Id, string Name);
public record RouteModel(Guid Id, DateTimeOffset DepartAt, DateTimeOffset ArriveBy, LocationModel From, LocationModel To, double Distance, double Price, CompanyModel Company)
{
    public ReservedRoute ToDomainObject()
    {
        return new ReservedRoute(Id, new Location(From.Id, From.Name), new Location(To.Id, To.Name), DepartAt, ArriveBy, new Company(Company.Id, Company.Name), Distance, Price);
    }
}
using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TravelInEstonia.Domain.Features.Schedules;

namespace TravelInEstonia.Services.Services.Models;

public class BusScheduleResponseModel
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("expires")] public ScheduleDateTime Expires { get; set; }

    [JsonProperty("routes")] public List<RouteResponseModel> Routes { get; set; }
}

public class RouteResponseModel
{
    [JsonProperty("id")] public Guid Id { get; set; }
    [JsonProperty("from")] public LocationResponseModel From { get; set; }
    [JsonProperty("to")] public LocationResponseModel To { get; set; }
    [JsonProperty("distance")] public double Distance { get; set; }
    [JsonProperty("schedule")] public List<ScheduleResponseModel> Schedule { get; set; }

    public Route ToDomainObject()
    {
        return new Route(Id, From.ToDomainObject(), To.ToDomainObject(), Distance, Schedule.Select(x => x.ToDomainObject()).ToImmutableList());
    }
}

public class LocationResponseModel
{
    [JsonProperty("id")] public Guid Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }

    public Location ToDomainObject()
    {
        return new Location(Id, Name);
    }
}

public class ScheduleResponseModel
{
    [JsonProperty("id")] public Guid Id { get; set; }
    [JsonProperty("price")] public double Price { get; set; }
    [JsonProperty("start")] public ScheduleDateTime Start { get; set; }
    [JsonProperty("end")] public ScheduleDateTime End { get; set; }
    [JsonProperty("company")] public CompanyResponseModel Company { get; set; }

    public Provider ToDomainObject()
    {
        return new Provider(Id, Price, Start.ToDateTimeOffset(), End.ToDateTimeOffset(), new Company(Company.Id, Company.State));
    }
}

public class CompanyResponseModel
{
    [JsonProperty("id")] public Guid Id { get; set; }

    [JsonProperty("state")] public string State { get; set; }
}

public class ScheduleDateTime
{
    [JsonProperty("date")] public string Date { get; set; }

    [JsonProperty("timezone_type")] public int Timezone_type { get; set; }

    [JsonProperty("timezone")] public string Timezone { get; set; }

    public DateTimeOffset ToDateTimeOffset()
    {
        if (!DateTime.TryParseExact(Date, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
        {
            throw new FormatException("Invalid date format");
        }

        return new DateTimeOffset(parsedDateTime);
    }
}
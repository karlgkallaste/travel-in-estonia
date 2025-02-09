namespace TravelInEstonia.App.Features.Schedules.Models;

public class ScheduleFiltersModel
{
    public string From { get; set; }
    public string To { get; set; }
    public DateTimeOffset DepartureDate { get; set; }
}
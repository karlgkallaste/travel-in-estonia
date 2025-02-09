using Marten;
using Microsoft.AspNetCore.Mvc;
using TravelInEstonia.App.Features.Schedules.Models;
using TravelInEstonia.Domain.Features.Schedules;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.App.Features.Schedules.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    [HttpGet("locations")]
    [ProducesResponseType(typeof(LocationModel[]), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> Locations([FromServices] IQuerySession documentSession)
    {
        var latestSchedule = await documentSession.Query<Schedule>()
            .OrderByDescending(x => x.ValidUntil).FirstOrDefaultAsync();
        if (latestSchedule == null)
        {
            return NotFound(Result.Failure("Schedule not found"));
        }

        var locations = latestSchedule.Routes.SelectMany(x => new[]
            {
                new LocationModel(x.From.Id, x.From.Name),
                new LocationModel(x.To.Id, x.To.Name)
            }).DistinctBy(x => x.Name)
            .ToArray();

        return Ok(locations);
    }


    [HttpGet("fares")]
    [ProducesResponseType(typeof(FareModel[]), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> Fares([FromServices] IQuerySession documentSession, [FromServices] IScheduleFareProvider fareProvider, [FromQuery] ScheduleFiltersModel filters)
    {
        var latestSchedule = await documentSession.Query<Schedule>()
            .OrderByDescending(x => x.ValidUntil)
            .FirstOrDefaultAsync();
        if (latestSchedule == null)
        {
            return NotFound(Result.Failure("Schedule not found"));
        }

        return Ok(fareProvider.Provide(latestSchedule, filters));
    }
}
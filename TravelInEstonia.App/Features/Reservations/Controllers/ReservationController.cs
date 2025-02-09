using System.Collections.Immutable;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelInEstonia.App.Features.Reservations.Models;
using TravelInEstonia.Domain.Features.Reservations.Commands;
using TravelInEstonia.Domain.Features.Schedules;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.App.Features.Reservations.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    [HttpPost("")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> Create([FromServices] IQuerySession documentSession, [FromServices] IMediator mediator, [FromBody] CreateReservationModel request)
    {
        var latestSchedule = await documentSession.Query<Schedule>()
            .OrderByDescending(x => x.ValidUntil).FirstOrDefaultAsync();
        if (latestSchedule == null)
        {
            return NotFound(Result.Failure("Schedule not found"));
        }

        if (latestSchedule.Id != request.Fare.ScheduleId)
        {
            return BadRequest(Result.Failure("Outdated prices"));
        }
        
        var commandResult = await mediator.Send(request.ToCommand());
        if (!commandResult.IsSuccess)
        {
            return BadRequest(Result.Failure("Failed to create reservation"));
        }

        return Ok();
    }
}
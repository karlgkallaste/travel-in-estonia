using System.Collections.Immutable;
using MediatR;
using TravelInEstonia.App.Features.Schedules.Models;
using TravelInEstonia.Domain.Features.Reservations.Commands;
using TravelInEstonia.Domain.Features.Schedules;

namespace TravelInEstonia.App.Features.Reservations.Models;

public class CreateReservationModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public FareModel Fare { get; set; }

    public CreateReservationCommand ToCommand()
    {
        return new CreateReservationCommand(Fare.ScheduleId, FirstName, LastName,
            Fare.Routes.Select(x => x.ToDomainObject()).ToImmutableList());
    }
}
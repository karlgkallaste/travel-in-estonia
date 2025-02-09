using System.Collections.Immutable;
using Marten;
using MediatR;
using TravelInEstonia.Domain.Features.Reservations.Events;
using TravelInEstonia.Domain.Features.Schedules;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Reservations.Commands;

public class CreateReservationCommand : IRequest<Result>
{
    public Guid ScheduleId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public ImmutableList<ReservedRoute> Routes { get; private set; }

    public CreateReservationCommand(Guid scheduleId, string firstName, string lastName, ImmutableList<ReservedRoute> routes)
    {
        ScheduleId = scheduleId;
        FirstName = firstName;
        LastName = lastName;
        Routes = routes;
    }

    public class Handler : IRequestHandler<CreateReservationCommand, Result>
    {
        private readonly IDocumentSession _documentSession;

        public Handler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<Result> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var latestSchedule = await _documentSession.Query<Schedule>()
                .Where(x => x.Id == request.ScheduleId)
                .FirstOrDefaultAsync(token: cancellationToken);
            if (latestSchedule == null)
            {
                return Result.Failure("Schedule not found");
            }

            if (request.Routes.Select(x => x.Id).Any(id => !latestSchedule.Routes.Select(x => x.Id).Contains(id)))
            {
                return Result.Failure("Invalid routes");
            }

            var @event = new ReservationCreated(Guid.NewGuid(), request.ScheduleId, request.FirstName, request.LastName, request.Routes);

            _documentSession.Events.Append(@event.Id, @event);
            await _documentSession.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
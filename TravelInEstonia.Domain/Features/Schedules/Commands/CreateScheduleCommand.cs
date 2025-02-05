using System.Collections.Immutable;
using Marten;
using MediatR;
using TravelInEstonia.Domain.Features.Schedules.Events;
using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Domain.Features.Schedules.Commands;

public class CreateScheduleCommand(Guid id, DateTimeOffset expiresAt, ImmutableList<Route> routes) : IRequest<Result>
{
    public Guid Id { get; private set; } = id;
    public DateTimeOffset ExpiresAt { get; private set; } = expiresAt;
    public ImmutableList<Route> Routes { get; private set; } = routes;

    public class Handler : IRequestHandler<CreateScheduleCommand, Result>
    {
        private readonly IDocumentSession _documentSession;

        public Handler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<Result> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            _documentSession.Events.Append(request.Id, new ScheduleCreated(request.Id, request.ExpiresAt, request.Routes));
            await _documentSession.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
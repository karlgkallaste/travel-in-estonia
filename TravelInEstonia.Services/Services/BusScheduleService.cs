using TravelInEstonia.Domain.Infrastructure;

namespace TravelInEstonia.Services.Services;

public interface IBusScheduleService
{
    Task<Result> GetLatestSchedule();
}

public class BusScheduleService : IBusScheduleService
{
    public Task<Result> GetLatestSchedule()
    {
        throw new NotImplementedException();
    }
}
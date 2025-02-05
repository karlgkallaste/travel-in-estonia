using System.Collections.Immutable;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TravelInEstonia.Domain.Features.Schedules.Commands;
using TravelInEstonia.Domain.Infrastructure;
using TravelInEstonia.Services.Services.Models;

namespace TravelInEstonia.Services.Services;

public interface IBusScheduleService
{
    Task<Result> GetLatestSchedule();
}

public class BusScheduleService : IBusScheduleService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<BusScheduleApiSettings> _apiSettings;
    private readonly ILogger<BusScheduleService> _logger;
    private readonly IMediator _mediator;

    public BusScheduleService(HttpClient httpClient, IOptions<BusScheduleApiSettings> apiSettings, ILogger<BusScheduleService> logger, IMediator mediator)
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Result> GetLatestSchedule()
    {
        var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_apiSettings.Value.UserName}:{_apiSettings.Value.Password}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

        var response = await _httpClient.GetAsync(_apiSettings.Value.Url);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.BadRequest)
        {
            _logger.LogError("[{Name}] Response status code: {StatusCode}. Content: {Content}", nameof(GetLatestSchedule), response.StatusCode, responseContent);
            return Result.Failure("Request failed");
        }

        var modelFromResponse = JsonSerializer.Deserialize<BusScheduleResponseModel>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (modelFromResponse == null)
        {
            _logger.LogError("[{Name}] Failed to deserialize. Content: {Content}", nameof(GetLatestSchedule), responseContent);
            return Result.Failure("Failed to deserialize");
        }

        var scheduleRoutes = modelFromResponse.Routes.Select(x => x.ToDomainObject()).ToImmutableList();
        var commandResult = await _mediator.Send(new CreateScheduleCommand(modelFromResponse.Id, modelFromResponse.Expires.ToDateTimeOffset(), scheduleRoutes));
        if (!commandResult.IsSuccess)
        {
            return Result.Failure("Failed to create schedule");
        }

        return Result.Success();
    }
}
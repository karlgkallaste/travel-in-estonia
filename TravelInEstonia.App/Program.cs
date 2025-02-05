using Marten;
using Marten.Events.Projections;
using TravelInEstonia.Domain;
using TravelInEstonia.Domain.Features.Schedules;
using TravelInEstonia.Services;
using TravelInEstonia.Services.Services;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.Configure<BusScheduleApiSettings>(builder.Configuration.GetSection("BusScheduleApiSettings"));
builder.Services.AddHttpClient();


builder.Services
    .RegisterDomainServices()
    .RegisterServicesServices();

builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
    // Specify that we want to use STJ as our serializer
    options.UseNewtonsoftForSerialization();
    options.Projections.Snapshot<Schedule>(SnapshotLifecycle.Inline);
    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
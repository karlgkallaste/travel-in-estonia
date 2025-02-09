using Marten;
using Marten.Events.Projections;
using TravelInEstonia.App.Features.Schedules.Models;
using TravelInEstonia.Domain;
using TravelInEstonia.Domain.Features.Reservations;
using TravelInEstonia.Domain.Features.Schedules;
using TravelInEstonia.Services;
using TravelInEstonia.Services.Services;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.Configure<BusScheduleApiSettings>(builder.Configuration.GetSection("BusScheduleApiSettings"));
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://frontend:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddTransient<IScheduleFareProvider, ScheduleFareProvider>();
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
    options.Projections.Snapshot<Reservation>(SnapshotLifecycle.Inline);
    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
}).UseLightweightSessions();

var app = builder.Build();
app.UseCors("AllowVite");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
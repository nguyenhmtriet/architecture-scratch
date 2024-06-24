using ArchitectureScratch.RateLimiters.Extensions;
using ArchitectureScratch.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Services
    .ConfigureRateLimiter()
    .ConfigureSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseCors(policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader();
    })
    .UseRouting()
    .UseRateLimiter()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });

static string GetTicks() => (DateTime.Now).ToString("T");

app.MapGet("/", () => Results.Ok($"Sliding Window Limiter {GetTicks()}"))
    .RequireRateLimiting("sliding");

app.Run();

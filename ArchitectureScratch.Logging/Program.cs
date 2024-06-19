using Logging.Extensions;
using Logging.Services;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSerilog();

builder.Services.AddScoped<AlwaysLogAsMessageTemplate>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/weatherforecast", (httpContext) =>
    {
        var service = httpContext.RequestServices.GetRequiredService<AlwaysLogAsMessageTemplate>();
        var result = service.GetWeatherForecast();
        httpContext.Response.WriteAsJsonAsync(result);
        return Task.CompletedTask;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
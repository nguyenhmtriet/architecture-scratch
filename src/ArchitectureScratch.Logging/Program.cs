using Logging.Extensions;
using Logging.Services;
using ArchitectureScratch.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services
    .AddScoped<AlwaysLogAsMessageTemplate>()
    .ConfigureSerilog()
    .ConfigureSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseRouting()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
using ArchitectureScratch.NSwagHttpClients.Configuration;
using ArchitectureScratch.NSwagHttpClients.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddOptions<HttpClientsConfiguration>()
    .Bind(builder.Configuration.GetSection("HttpClients"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.ConfigureSwagger();
builder.Services.ConfigureHttpClientApis();


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
using System.Text.Json.Serialization;
using ArchitectureScratch.MultipleAuthenticationSchemes.Extensions;
using ArchitectureScratch.MultipleAuthenticationSchemes.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddRazorPages();

builder.Services
    .ConfigureApiVersioning()
    .AddAppSettingsOption(builder.Configuration)
    .ConfigureSwagger(builder.Configuration)
    .ConfigureSecurity(builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwaggerMiddleware();

app.UseHttpsRedirection();

app
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapFallbackToPage("/Index/Index");

app.Run();
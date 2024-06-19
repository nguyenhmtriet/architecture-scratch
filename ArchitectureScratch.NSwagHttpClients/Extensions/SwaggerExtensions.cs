﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchitectureScratch.NSwagHttpClients.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.MapType<DateOnly>(() => new OpenApiSchema 
            { 
                Type = "string", 
                Format = "date",
                Example = OpenApiAnyFactory.CreateFromJson("\"2021-09-01\"")
            });
            swaggerGenOptions.MapType<TimeOnly>(() => new OpenApiSchema 
            { 
                Type = "string", 
                Format = "time", 
                Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"") 
            }); 
        });

        return services;
    }
}
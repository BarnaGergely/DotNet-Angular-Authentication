﻿using AuthApp.Server.Common.Api;

namespace AuthApp.Server.WeatherForecast.Endpoints;

public class GetWeatherForecast : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async () =>
        {
            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                       new WeatherForecast
                       (
                           DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                           Random.Shared.Next(-20, 55),
                           summaries[Random.Shared.Next(summaries.Length)]
                       ))
                       .ToArray();
            return forecast;
        })
        .WithName("GetPaymentById");
    }

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

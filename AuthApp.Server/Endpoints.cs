using AuthApp.Server.Common.Api;
using AuthApp.Server.Payments.Endpoints;
using AuthApp.Server.WeatherForecast.Endpoints;
using Microsoft.AspNetCore.Identity;

namespace AuthApp.Server;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var apiGroup = app.MapGroup("/api");

        if (app.Environment.IsDevelopment())
        {
            apiGroup.MapOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        }
        apiGroup//.RequireAuthorization()
            .MapPaymentsEndpoints();
        apiGroup.MapWeatherForecastEndpoints();
        apiGroup.MapIdentityApi<IdentityUser>();
    }

    private static void MapPaymentsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/payments")
            .WithTags("Payments");
        endpoints.MapPublicGroup()
            .MapEndpoint<GetPayments>()
            .MapEndpoint<GetPaymentById>()
            .MapEndpoint<CreatePayment>()
            .MapEndpoint<UpdatePayment>()
            .MapEndpoint<DeletePayment>();

    }

    private static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/weatherforecast")
            .WithTags("WeatherForecast");
        endpoints.MapPublicGroup()
            .MapEndpoint<GetWeatherForecast>();
    }

    private static RouteGroupBuilder MapPublicGroup(this IEndpointRouteBuilder app, string? prefix = null)
    {
        return app.MapGroup(prefix ?? string.Empty)
            .AllowAnonymous();
    }

    private static RouteGroupBuilder MapAuthorizedGroup(this IEndpointRouteBuilder app, string? prefix = null)
    {
        return app.MapGroup(prefix ?? string.Empty)
            .RequireAuthorization()
            .WithOpenApi(x => new(x)
            {
                // TODO: Security = [new() { [securityScheme] = [] }],
            });
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

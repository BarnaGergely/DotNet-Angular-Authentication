using AuthApp.Server.Common.Api;
using AuthApp.Server.Payments.Endpoints;
using AuthApp.Server.WeatherForecast.Endpoints;

namespace AuthApp.Server;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/api")
            .MapPaymentsEndpoints()
            .MapWeatherForecastEndpoints();
    }

    private static RouteGroupBuilder MapPaymentsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/payments")
            .WithTags("Payments");
        endpoints.MapPublicGroup()
            .MapEndpoint<GetPayments>()
            .MapEndpoint<GetPaymentById>()
            .MapEndpoint<CreatePayment>()
            .MapEndpoint<UpdatePayment>()
            .MapEndpoint<DeletePayment>();
        return endpoints;

    }

    private static RouteGroupBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/weatherforecast")
            .WithTags("WeatherForecast");
        endpoints.MapPublicGroup()
            .MapEndpoint<GetWeatherForecast>();
        return endpoints;
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

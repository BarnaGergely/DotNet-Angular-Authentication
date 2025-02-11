using AuthApp.Server.Authentication.Endpoints;
using AuthApp.Server.Common.Api;
using AuthApp.Server.Data.Models;
using AuthApp.Server.Payments.Endpoints;
using AuthApp.Server.WeatherForecast.Endpoints;

namespace AuthApp.Server;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {

        app.MapOpenApiEndpoints();

        var apiGroup = app.MapGroup("/api");
        apiGroup//.RequireAuthorization()
            .MapPaymentsEndpoints();
        apiGroup.MapWeatherForecastEndpoints();
        apiGroup.MapAuthenticationEndpoints();
    }

    private static void MapOpenApiEndpoints(this WebApplication app)
    {
        app.MapOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        if (app.Environment.IsDevelopment())
        {

            // In the future we should use Scalar for production environments
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/using-openapi-documents?view=aspnetcore-9.0
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

        }
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

    private static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/auth")
            .WithTags("Authentication");
        endpoints.MapIdentityApi<AppUser>();
        endpoints.MapEndpoint<Signup>();
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

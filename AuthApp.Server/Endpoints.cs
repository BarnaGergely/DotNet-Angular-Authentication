﻿using AuthApp.Server.Common.Api;
using AuthApp.Server.Payments.Endpoints;

namespace AuthApp.Server;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPaymentsEndpoints();
    }

    private static void MapPaymentsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/weatherforecast")
            .WithTags("Payments");
        endpoints.MapPublicGroup()
            .MapEndpoint<GetPaymentById>();
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

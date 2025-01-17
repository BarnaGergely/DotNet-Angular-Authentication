﻿using AuthApp.Server.Common.Api;
using AuthApp.Server.Data;
using AuthApp.Server.Data.Models;

namespace AuthApp.Server.Payments.Endpoints;

public class GetPaymentById : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (AppDbContext db, int id) =>
        {
            var payment = await db.PaymentDetails.FindAsync(id);
            if (payment is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(payment);
        });
    }
}

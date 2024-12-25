using AuthApp.Server.Common.Api;
using AuthApp.Server.Data;
using AuthApp.Server.Data.Models;

namespace AuthApp.Server.Payments.Endpoints;

public class GetPayments : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (AppDbContext db) =>
        {
            var payment = db.PaymentDetails;
            if (payment is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(payment);
        });
    }
}

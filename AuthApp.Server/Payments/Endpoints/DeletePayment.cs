using AuthApp.Server.Common.Api;
using AuthApp.Server.Data;

namespace AuthApp.Server.Payments.Endpoints;

public class DeletePayment : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (AppDbContext db, int id) =>
        {
            var paymentDetail = await db.PaymentDetails.FindAsync(id);
            if (paymentDetail is null)
            {
                return Results.NotFound();
            }

            db.PaymentDetails.Remove(paymentDetail);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}

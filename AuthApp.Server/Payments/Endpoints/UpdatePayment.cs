using AuthApp.Server.Common.Api;
using AuthApp.Server.Data;
using AuthApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Server.Payments.Endpoints;

public class UpdatePayment : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        app.MapPut("/{id}", async (AppDbContext db, int id, PaymentDetail paymentDetail) =>
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                return Results.BadRequest();
            }

            db.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(db, id))
                {
                    return Results.NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Results.Ok(paymentDetail);
        });
    }

    private static bool PaymentDetailExists(AppDbContext db, int id)
    {
        return db.PaymentDetails.Any(e => e.PaymentDetailId == id);
    }
}

using AuthApp.Server.Common.Api;
using AuthApp.Server.Data;
using AuthApp.Server.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Server.Payments.Endpoints;

public class CreatePayment : IEndpoint
{
    // TODO: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (AppDbContext db, PaymentDetail paymentDetail) =>
            {
                db.PaymentDetails.Add(paymentDetail);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
                    {
                        return Results.Conflict("A paymentDetail with the same ID already exists.");
                    }
                    return Results.BadRequest();
                }
                return TypedResults.Created<PaymentDetail>($"/{paymentDetail.PaymentDetailId}", paymentDetail);
            });
    }
}

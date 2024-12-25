using AuthApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Server.Data;

public class AppDbContext : DbContext
{
    public DbSet<PaymentDetail> PaymentDetails { get; set; }

    public string DbPath { get; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "AuthApp.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}") // Configures EF to create a Sqlite database file in the special "local" folder for your platform.
        .UseSeeding((context, _) =>
        {
            var testPayment = context.Set<PaymentDetail>().FirstOrDefault();
            if (testPayment is null)
            {
                context.Set<PaymentDetail>().Add(new PaymentDetail
                {
                    PaymentDetailId = 1,
                    CardOwnerName = "John Doe",
                    CardNumber = "1234567890123456",
                    ExpirationDate = "12/12",
                    SecurityCode = "123"
                });
                context.SaveChanges();
            }
        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var testPayment = await context.Set<PaymentDetail>().FirstOrDefaultAsync();
            if (testPayment is null)
            {
                context.Set<PaymentDetail>().Add(new PaymentDetail
                {
                    PaymentDetailId = 1,
                    CardOwnerName = "John Doe",
                    CardNumber = "1234567890123456",
                    ExpirationDate = "12/12",
                    SecurityCode = "123"
                });
                await context.SaveChangesAsync();
            }
        });
}

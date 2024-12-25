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

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

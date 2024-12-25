using AuthApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Server.Data;

public class AppDbContext : DbContext
{
    public DbSet<PaymentDetail> PaymentDetails { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

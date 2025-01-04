using AuthApp.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AuthApp.Server;
public static class ConfigureApp
{
    public static async Task Configure(this WebApplication app)
    {

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.MapStaticAssets();
        //app.UseAuthorization();
        app.MapEndpoints();
        app.MapFallbackToFile("/index.html");
        await app.EnsureDatabaseCreated();
        
    }

    private static async Task EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
}
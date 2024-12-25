using AuthApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AuthApp.Server;
public static class ConfigureApp
{
    public static async Task Configure(this WebApplication app)
    {

        app.UseSerilogRequestLogging();
        if (app.Environment.IsDevelopment())
            app.MapOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.MapStaticAssets();
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
using Microsoft.EntityFrameworkCore;

namespace Kowmal.WebApp.Persistance.Helpers;

public static class AppDbContextHelpers
{
    // Method to see the database. Should not be used in production: demo purposes only.
    // options: The configured options.
    // count: The number of contacts to seed.
    public static async Task EnsureDbCreatedAndSeedWithCountOfAsync(DbContextOptions<AppDbContext> options, int count)
    {
        // Empty to avoid logging while inserting (otherwise will flood console).
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<AppDbContext>(options)
            .UseLoggerFactory(factory);

        await using var context = new AppDbContext(builder.Options);
        
        // Result is true if the database had to be created.
        if (await context.Database.EnsureCreatedAsync() && (await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
            //var seed = new DataSeeder();
            //await seed.SeedDatabaseWithInitialDataAsync(context, count);
        }
    }
}
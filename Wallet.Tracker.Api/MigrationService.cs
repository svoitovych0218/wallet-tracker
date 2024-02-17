namespace Wallet.Tracker.Api;

using Wallet.Tracker.Domain.Services;
using Wallet.Tracker.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class MigrationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MigrationService started");
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                // Retrieve your DbContext and apply migrations
                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>() as PosgresDbContext;
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations != null && pendingMigrations.Count() > 0)
                    await dbContext.Database.MigrateAsync(cancellationToken);
                else
                    Console.WriteLine("There are no migrations to apply");
                Console.WriteLine("Migrations applied");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying database migrations: {ex.Message}");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

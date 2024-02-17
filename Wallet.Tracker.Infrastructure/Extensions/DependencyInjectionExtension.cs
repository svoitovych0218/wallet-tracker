namespace Wallet.Tracker.Infrastructure.Extensions;

using Wallet.Tracker.Domain.Services;
using Wallet.Tracker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<IDbContext, PosgresDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        });
        
        return services;
    }
}
namespace Wallet.Tracker.Infrastructure.CoinMarketCap.Extensions;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastructure.CoinMarketCap.Options;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddCoinMarketCapServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICoinMerketCapApiClient, CoinMarketCapApiClient>();
        services.Configure<CoinMarketCapApiOptions>(configuration.GetSection("CoinMarketCap"));

        //Potentially infrastructure layer
        return services;
    }
}

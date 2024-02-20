namespace Wallet.Tracker.Infrastruction.ChainExplorer.Extensions;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastruction.ChainExplorer.Factory;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddChainExplorerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IChainExplorerApiClientFactory, ChainExplorerApiClientFactory>();

        services.AddSingleton<Arbitrum.ArbiScanApiClient>();
        services.AddSingleton<Bsc.BscScanApiClient>();
        services.AddSingleton<Cronos.CronosScanApiClient>();
        services.AddSingleton<Ether.EtherScanApiClient>();
        services.AddSingleton<Fantom.FantomScanApiClient>();
        services.AddSingleton<Optimism.OptimismScanApiClient>();
        services.AddSingleton<Polygon.PolygonScanApiClient>();
        services.AddSingleton<Base.BaseScanApiClient>();

        services.Configure<Options.ArbitrumOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.BscOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.CronosOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.EtherOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.FantomOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.OptimismOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.PolygonOptions>(configuration.GetSection("ExplorerApiKeys"));
        services.Configure<Options.BaseOptions>(configuration.GetSection("ExplorerApiKeys"));

        return services;
    }
}

namespace Wallet.Tracker.Sqs;

using Wallet.Tracker.Domain.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wallet.Tracker.Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Wallet.Tracker.Infrastructure.CoinMarketCap.Extensions;
using Wallet.Tracker.Infrastruction.ChainExplorer.Extensions;
using Wallet.Tracker.Infrastructure.Telegram.Extensions;

public static class Startup
{
    public static ServiceProvider ServiceProvider => InitializeServiceProvider();
    public static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();

        IConfiguration configuration = builder.Build();
        services.AddLogging(s => s
            .AddJsonConsole(q =>
            {
                q.IncludeScopes = true;
            })
            .SetMinimumLevel(LogLevel.Information));

        services.AddDomainServices(configuration);
        services.AddInfrastructureServices();
        services.AddCoinMarketCapServices(configuration);
        services.AddChainExplorerServices(configuration);
        services.AddTelegramBotServices(configuration);

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        };

        return services;
    }

    private static ServiceProvider InitializeServiceProvider()
    {
        var services = ConfigureServices();
        return services.BuildServiceProvider();
    }
}
namespace Wallet.Tracker.Domain.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Tracker.Domain.Services.Queries;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetStreamsQuery).Assembly));
        services.Configure<MoralisApiKeyOption>(configuration.GetSection("MoralisApi"));
        services.AddSingleton<IMoralisStreamsApiClient, MoralisStreamsApiClient>();
        //Potentially infrastructure layer
        return services;
    }
}

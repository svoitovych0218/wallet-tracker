﻿namespace Wallet.Tracker.Sqs;

using Wallet.Tracker.Domain.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wallet.Tracker.Infrastructure.Extensions;

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
        return services;
    }

    private static ServiceProvider InitializeServiceProvider()
    {
        var services = ConfigureServices();
        return services.BuildServiceProvider();
    }
}
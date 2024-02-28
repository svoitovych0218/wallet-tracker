namespace Wallet.Tracker.Infrastructure.Telegram.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Tracker.Domain.Services.Services.Interfaces;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddTelegramBotServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITelegramBotNotificationService, BotNotificationService>();
        services.Configure<TelegramBotOptions>(configuration.GetSection("TelegramBot"));

        return services;
    }
}

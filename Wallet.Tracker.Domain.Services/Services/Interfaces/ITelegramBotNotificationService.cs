namespace Wallet.Tracker.Domain.Services.Services.Interfaces;

using Wallet.Tracker.Domain.Models.Entities;

public interface ITelegramBotNotificationService
{
    Task SendNotification(IEnumerable<long> userIds, string message);
    Task SendTransactionAlert(IEnumerable<long> userIds, IEnumerable<Erc20Transaction> transactions);
}

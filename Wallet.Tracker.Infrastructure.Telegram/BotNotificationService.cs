namespace Wallet.Tracker.Infrastructure.Telegram;

using global::Telegram.Bot;
using global::Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Models.Enums;
using Wallet.Tracker.Domain.Services.Services.Interfaces;

internal class BotNotificationService : ITelegramBotNotificationService
{

    private char[] SPECIAL_CHARS = new char[] {
        '\\',
        '_',
        '*',
        '[',
        ']',
        '(',
        ')',
        '~',
        '`',
        '>',
        '<',
        '&',
        '#',
        '+',
        '-',
        '=',
        '|',
        '{',
        '}',
        '.',
        '!'
    };

    private readonly TelegramBotClient _telegramBotClient;
    private readonly ILogger<BotNotificationService> _logger;

    public BotNotificationService(IOptions<TelegramBotOptions> options, ILogger<BotNotificationService> logger)
    {
        _telegramBotClient = new TelegramBotClient(options.Value.ApiKey);
        _logger = logger;
    }

    public async Task SendNotification(IEnumerable<long> userIds, string message)
    {
        var tasks = userIds.Select(async s =>
        {
            try
            {
                await _telegramBotClient.SendTextMessageAsync(s, message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error occured during notification send");
            }
        });

        await Task.WhenAll(tasks);
    }

    public async Task SendTransactionAlert(IEnumerable<long> userIds, IEnumerable<Erc20Transaction> transactions)
    {
        var data = transactions.GroupBy(s => new
        {
            s.WalletAddress,
            s.TxHash,
            s.At,
            s.TransactionChain.ChainId,
            s.TransactionChain.Chain.Name
        })
            .Select(s => new
            {
                TxHash = s.Key.TxHash,
                WalletAddress = s.Key.WalletAddress,
                At = s.Key.At,
                ChainId = s.Key.ChainId,
                ChainName = s.Key.Name,
                Transfers = s.GroupBy(q => new
                {
                    q.Token.Symbol,
                    q.Amount,
                    q.Token.Address,
                    q.Token.Name,
                    q.Token.ExistAtCoinmarketCap,
                    q.Token.ContractCodePublished,
                    q.TransferType,
                })
                .Select(q => new
                {
                    TokenSymbol = q.Key.Symbol,
                    Amount = q.Sum(s => s.Amount),
                    ContractAddress = q.Key.Address,
                    TokenName = q.Key.Name,
                    TransferType = q.Key.TransferType,
                    ExistsAtCoinMarketCap = q.Key.ExistAtCoinmarketCap,
                    ContractCodePublished = q.Key.ContractCodePublished,
                    UsdAmount = q.Sum(s => s.UsdValue),
                })
            });

        var message = "";

        foreach (var item in data)
        {
            message += $"Wallet: `{item.WalletAddress}`\n" +
                $"" + $"Chain: {item.ChainName}\n" +
                $"" + $"TxHash: `{item.TxHash}`\n\n" +
                $"" + $"Transfers: \n";

            foreach (var q in item.Transfers)
            {
                message += $"{(q.TransferType == TransferType.In ? "\U00002795" : "\U00002796")}" +
                        $" | " +
                        $"{q.TokenName}" +
                        $" | " +
                        $"{Math.Round(q.Amount, 2)} | {(q.UsdAmount == null ? "" : Math.Round(q.UsdAmount.Value, 2) + "$")} | " +
                        $"CMC: {(q.ExistsAtCoinMarketCap ? "\U00002705" : "\U0000274c")} | " +
                        $"Contract: {(q.ContractCodePublished ? "\U00002705" : "\U0000274c")}\n";
            }

            message += "\n\n" +
                       $"More details at: [link](https://wallet-tracker-web.pages.dev/transactions)";
        }

        foreach (var userId in userIds)
        {
            try
            {
                await _telegramBotClient.SendTextMessageAsync(userId, message, parseMode: ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error occured during send alerts");
            }
        }
    }
}

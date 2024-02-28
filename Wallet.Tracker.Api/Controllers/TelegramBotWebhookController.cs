namespace Wallet.Tracker.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Services.Interfaces;

[Route("api/telegram-webhook")]
public class TelegramBotWebhookController : ControllerBase
{
    private readonly ILogger<TelegramBotWebhookController> _logger;
    private readonly IMediator _mediator;
    private readonly ITelegramBotNotificationService _botNotificationService;

    public TelegramBotWebhookController(
        ILogger<TelegramBotWebhookController> logger,
        IMediator mediator,
        ITelegramBotNotificationService botNotificationService)
    {
        _logger = logger;
        _mediator = mediator;
        _botNotificationService = botNotificationService;
    }


    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody]Update update)
    {
        _logger.LogInformation($"Telegram Webhook started. {update?.Message?.From?.Id}");
        if (update?.Message?.Text == "/start" && update.Message?.From?.Id != null)
        {
            var command = new AddTelegramUserReportSubscriptionCommand(update.Message.From.Id, update.Message.From.Username);
            await _mediator.Send(command);
            await _botNotificationService.SendNotification(new long[] { update!.Message!.Chat!.Id }, "You are subscribed wallets");
        }

        return Ok();
    }
}

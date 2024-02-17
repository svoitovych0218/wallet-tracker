namespace Wallet.Tracker.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Tracker.Api.Controllers.RequestModels;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Queries;

[Route("api/webhook")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(IMediator mediator, ILogger<WebhookController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        var res = await _mediator.Send(new GetStreamsQuery());
        return Ok(res);
    }

    [HttpPost("erc-transfer")]
    public async Task<IActionResult> ErcTransferWebhook([FromBody] Erc20WebhookRequestModel request)
    {
        if (request.ChainId == "")
        {
            return Ok();
        }
        var command = new AddErc20TransferCommand(
            request.Confirmed,
            request.ChainId,
            new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(request.Block.Timestamp)),
            request.Erc20Transfers.Select(s => new Erc20TransferModel(
                s.TransactionHash,
                s.Contract,
                s.From,
                s.To,
                s.Value,
                s.TokenName,
                s.TokenSymbol,
                int.Parse(s.TokenDecimals),
                decimal.Parse(s.ValueWithDecimals),
                s.PossibleSpam
                )));

        await _mediator.Send(command);
        return Ok();
    }
}

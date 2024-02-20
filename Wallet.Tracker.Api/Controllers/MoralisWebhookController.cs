namespace Wallet.Tracker.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wallet.Tracker.Api.Controllers.RequestModels;
using Wallet.Tracker.Api.Services;
using Wallet.Tracker.SQS.Contracts;

[Route("api/webhook")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WebhookController> _logger;
    private readonly ISqsClient _sqsClient;

    public WebhookController(
        IMediator mediator,
        ILogger<WebhookController> logger,
        ISqsClient sqsClient)
    {
        _mediator = mediator;
        _logger = logger;
        _sqsClient = sqsClient;
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        //await _sqsClient.SendErc20TransferMessage(null);
        return Ok();
    }

    [HttpPost("erc-transfer")]
    public async Task<IActionResult> ErcTransferWebhook([FromBody] Erc20WebhookRequestModel request)
    {
        _logger.LogInformation("Deserialized request: " + JsonConvert.SerializeObject(request));

        if (request.ChainId == "" || request.Confirmed)
        {
            return Ok();
        }

        var message = new AddErc20TransferSqsMessage
        {
            Confirmed = request.Confirmed,
            ChainId = request.ChainId,
            At = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(int.Parse(request.Block.Timestamp)),
            Erc20Transfers = request.Erc20Transfers.Select(s => new Erc20TransferSqsModel
            {
                TransactionHash = s.TransactionHash,
                Contract = s.Contract,
                From = s.From,
                To = s.To,
                Value = s.Value,
                TokenName = s.TokenName,
                TokenSymbol = s.TokenSymbol,
                TokenDecimals = int.Parse(s.TokenDecimals),
                ValueWithDecimals = decimal.Parse(s.ValueWithDecimals),
                PossibleSpam = s.PossibleSpam
            })
        };
        await _sqsClient.SendErc20TransferMessage(message);
        return Ok();
    }
}

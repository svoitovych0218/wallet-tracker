namespace Wallet.Tracker.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.Tracker.Api.Controllers.RequestModels;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Queries;

[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-stream")]
    public async Task<IActionResult> CreateStream([FromBody]CreateStreamRequestModel request)
    {
        var res = await _mediator.Send(new AddStreamCommand(request.WalletAddress, request.Title, request.ChainId));
        return Ok(res);
    }

    [HttpGet("get-transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var res = await _mediator.Send(new GetTransfersQuery());
        return Ok(res);
    }
}

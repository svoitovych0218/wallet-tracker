namespace Wallet.Tracker.Domain.Services.CommandHandlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public class AddStreamCommandHandler : IRequestHandler<AddStreamCommand, Unit>
{
    private readonly IDbContext _dbContext;
    private readonly IMoralisStreamsApiClient _moralisStreamsApiClient;
    private readonly ILogger<AddStreamCommandHandler> _logger;

    public AddStreamCommandHandler(
        IDbContext dbContext,
        IMoralisStreamsApiClient moralisStreamsApiClient,
        ILogger<AddStreamCommandHandler> logger)
    {
        _dbContext = dbContext;
        _moralisStreamsApiClient = moralisStreamsApiClient;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddStreamCommand request, CancellationToken cancellationToken)
    {
        var chain = await _dbContext.GetQuery<Chain>().FirstOrDefaultAsync(s => s.Id == request.ChainId);
        if (chain == null)
        {
            throw new CustomException("Chain was not found", 400);
        }

        var wallet = new WalletData(request.Address.ToLower(), request.Title, DateTime.UtcNow);
        wallet.TrackingChains.Add(new WalletChain(chain.Id, wallet.Address));

        var streamId = await _moralisStreamsApiClient.CreateStream(new CreateStreamRequest(request.Address, request.ChainId));
        wallet.MoralisStreamId = streamId;

        _dbContext.Add(wallet);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

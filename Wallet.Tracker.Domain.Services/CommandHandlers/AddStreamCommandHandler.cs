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
        _logger.LogInformation($"{nameof(AddStreamCommandHandler)} started");

        var chains = await _dbContext.GetQuery<Chain>().Where(s => request.ChainIds.Contains(s.Id)).ToListAsync(cancellationToken);
        var notSupportedChains = request.ChainIds.Where(s => !chains.Any(q => q.Id == s));
        
        if (notSupportedChains.Any())
        {
            throw new CustomException($"Chain Ids: [{string.Join(", ", notSupportedChains)}] were not found", 400);
        }

        var address = request.Address.ToLower();

        var existingWallet = await _dbContext
            .GetQuery<WalletData>()
            .Include(s => s.TrackingChains)
            .FirstOrDefaultAsync(s => s.Address == address && s.IsDeleted);

        if (existingWallet != null)
        {
            existingWallet.IsDeleted = false;

            var newStreamId = await _moralisStreamsApiClient.CreateStream(new CreateStreamRequest(request.Address, request.ChainIds, request.Title));
            existingWallet.MoralisStreamId = newStreamId;

            existingWallet.TrackingChains.RemoveAll(s => !request.ChainIds.Contains(s.ChainId));
            var chainIdsToAdd = request.ChainIds.Where(s => !existingWallet.TrackingChains.Any(q => q.ChainId == s));

            if (chainIdsToAdd.Any())
            {
                existingWallet.TrackingChains.AddRange(chainIdsToAdd.Select(s => new WalletChain(s, existingWallet.Address)));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        var wallet = new WalletData(address, request.Title, DateTime.UtcNow);
        wallet.TrackingChains.AddRange(request.ChainIds.Select(s => new WalletChain(s, wallet.Address)));

        var streamId = await _moralisStreamsApiClient.CreateStream(new CreateStreamRequest(request.Address, request.ChainIds, request.Title));
        wallet.MoralisStreamId = streamId;

        _dbContext.Add(wallet);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"{nameof(AddStreamCommandHandler)} finished");

        return Unit.Value;
    }
}

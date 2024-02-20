namespace Wallet.Tracker.Domain.Services.CommandHandlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient;

public class DeleteStreamCommandHandler : IRequestHandler<DeleteStreamCommand, Unit>
{
    private readonly IDbContext _dbContext;
    private readonly IMoralisStreamsApiClient _moralisStreamsApiClient;

    public DeleteStreamCommandHandler(IDbContext dbContext, IMoralisStreamsApiClient moralisStreamsApiClient)
    {
        _dbContext = dbContext;
        _moralisStreamsApiClient = moralisStreamsApiClient;
    }

    public async Task<Unit> Handle(DeleteStreamCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _dbContext.GetQuery<WalletData>().FirstOrDefaultAsync(s => s.Address == request.Address);

        if (wallet == null)
        {
            return Unit.Value;
        }

        await _moralisStreamsApiClient.DeleteStream(wallet.MoralisStreamId.ToString());
        wallet.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

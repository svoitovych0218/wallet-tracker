namespace Wallet.Tracker.Domain.Services.QueryHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Queries;

public class GetWalletsQueryHandler : IRequestHandler<GetWalletsQuery, GetWalletsQueryResult>
{
    private readonly IDbContext _dbContext;

    public GetWalletsQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetWalletsQueryResult> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.GetQuery<WalletData>()
            .Where(s => !s.IsDeleted)
            .Select(s => new WalletViewModel
            {
                Address = s.Address,
                Title = s.Title,
                ChainIds = s.TrackingChains.Select(q => q.Chain.Id),
                NotificationsCount = s.Erc20Transactions.Count(),
                CreatedAt = s.CreatedAt,
            });

        var result = await query.ToListAsync(cancellationToken);

        return new GetWalletsQueryResult { Wallets = result };
    }
}

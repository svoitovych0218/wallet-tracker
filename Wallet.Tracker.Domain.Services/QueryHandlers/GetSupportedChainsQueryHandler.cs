namespace Wallet.Tracker.Domain.Services.QueryHandlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Queries;

public class GetSupportedChainsQueryHandler : IRequestHandler<GetSupportedChainsQuery, GetSupportedChainsQueryResult>
{
    private readonly IDbContext _dbContext;

    public GetSupportedChainsQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetSupportedChainsQueryResult> Handle(GetSupportedChainsQuery request, CancellationToken cancellationToken)
    {
        var chains = await _dbContext.GetQuery<Chain>().Select(s => new ChainData(s.Id, s.Name)).ToListAsync(cancellationToken);

        return new GetSupportedChainsQueryResult(chains);
    }
}

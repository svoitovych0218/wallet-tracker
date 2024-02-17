namespace Wallet.Tracker.Domain.Services.QueryHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Queries;

public class GetTransfersQueryHandler : IRequestHandler<GetTransfersQuery, GetTransfersQueryResult>
{
    private readonly IDbContext _dbContext;

    public GetTransfersQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTransfersQueryResult> Handle(GetTransfersQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.GetQuery<Erc20Transaction>()
            .GroupBy(s => new { s.WalletAddress, s.TxHash, s.At })
            .Select(s => new TransactionData
            {
                TxHash = s.Key.TxHash,
                WalletAddress = s.Key.WalletAddress,
                At = s.Key.At,
                Transfers = s.Select(q => new TransferData
                {
                    TokenSymbol = q.Symbol,
                    Amount = q.Amount,
                    ContractAddress = q.ContractAddress,
                    TokenName = q.TokenName,
                    TransferType = q.TransferType,
                })
            });

        var result = await query.ToListAsync(cancellationToken);

        return new GetTransfersQueryResult(result);
    }
}

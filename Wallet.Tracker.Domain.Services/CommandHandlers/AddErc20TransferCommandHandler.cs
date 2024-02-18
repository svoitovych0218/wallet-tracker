namespace Wallet.Tracker.Domain.Services.CommandHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Models.Enums;
using Wallet.Tracker.Domain.Services.Commands;

public class AddErc20TransferCommandHandler : IRequestHandler<AddErc20TransferCommand, Unit>
{
    private readonly IDbContext _dbContext;
    private readonly ILogger<AddErc20TransferCommandHandler> _logger;

    public AddErc20TransferCommandHandler(
        IDbContext dbContext,
        ILogger<AddErc20TransferCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<Unit> Handle(AddErc20TransferCommand request, CancellationToken cancellationToken)
    {
        var allPossibleWalletIds = request.Erc20Transfers.Select(s => s.From).Union(request.Erc20Transfers.Select(s => s.To));
        var allWallets = await _dbContext.GetQuery<WalletData>().Where(s => allPossibleWalletIds.Contains(s.Address)).ToListAsync(cancellationToken);
        
        foreach (var transfer in request.Erc20Transfers)
        {
            var walletReceiver = allWallets.FirstOrDefault(s => s.Address == transfer.To);
            if (walletReceiver != null)
            {
                var erc20TransactionId = Guid.NewGuid();
                var tx = new Erc20Transaction(
                    erc20TransactionId,
                    transfer.To,
                    transfer.TransactionHash,
                    request.At,
                    TransferType.In,
                    transfer.TokenSymbol,
                    transfer.TokenName,
                    transfer.Value,
                    transfer.Contract,
                    transfer.ValueWithDecimals);

                tx.TransactionChain = new Erc20TransactionChain(erc20TransactionId, request.ChainId);
                _dbContext.Add(tx);
            }

            var walletSender = allWallets.FirstOrDefault(s => s.Address == transfer.From);
            if (walletSender != null)
            {
                var erc20TransactionId = Guid.NewGuid();
                var tx = new Erc20Transaction(
                    erc20TransactionId,
                    transfer.From,
                    transfer.TransactionHash,
                    request.At,
                    TransferType.Out,
                    transfer.TokenSymbol,
                    transfer.TokenName,
                    transfer.Value,
                    transfer.Contract,
                    transfer.ValueWithDecimals);

                tx.TransactionChain = new Erc20TransactionChain(erc20TransactionId, request.ChainId);
                _dbContext.Add(tx);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

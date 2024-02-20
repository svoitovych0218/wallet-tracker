namespace Wallet.Tracker.Domain.Services.CommandHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Models.Enums;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.Domain.Services.Services.Interfaces;

public class AddErc20TransferCommandHandler : IRequestHandler<AddErc20TransferCommand, Unit>
{
    private readonly IDbContext _dbContext;
    private readonly ICoinMerketCapApiClient _coinMerketCapApiClient;
    private readonly IChainExplorerApiClientFactory _chainExplorerApiClientFactory;
    private readonly ILogger<AddErc20TransferCommandHandler> _logger;

    public AddErc20TransferCommandHandler(
        IDbContext dbContext,
        ICoinMerketCapApiClient coinMerketCapApiClient,
        IChainExplorerApiClientFactory chainExplorerApiClientFactory,
        ILogger<AddErc20TransferCommandHandler> logger)
    {
        _dbContext = dbContext;
        _coinMerketCapApiClient = coinMerketCapApiClient;
        _chainExplorerApiClientFactory = chainExplorerApiClientFactory;
        _logger = logger;
    }
    public async Task<Unit> Handle(AddErc20TransferCommand request, CancellationToken cancellationToken)
    {
        var allPossibleWalletIds = request.Erc20Transfers.Select(s => s.From).Union(request.Erc20Transfers.Select(s => s.To));
        var allWallets = await _dbContext.GetQuery<WalletData>().Where(s => allPossibleWalletIds.Contains(s.Address)).ToListAsync(cancellationToken);

        foreach (var transfer in request.Erc20Transfers)
        {
            var tokenAddress = transfer.Contract.ToLower();
            var token = await _dbContext.GetQuery<Erc20Token>().FirstOrDefaultAsync(s => s.Address == tokenAddress && s.ChainId == request.ChainId);
            var cmcInfo = await _coinMerketCapApiClient.GetTokenInfo(tokenAddress);

            if (token == null)
            {
                token = await CreateErc20Token(request, transfer, tokenAddress, cmcInfo.IsExists);
                _dbContext.Add(token);
            }

            var walletReceiver = allWallets.FirstOrDefault(s => s.Address == transfer.To);
            if (walletReceiver != null)
            {
                var tx = CreateErc20Transaction(request, transfer, cmcInfo.CurrentPriceUsd * transfer.ValueWithDecimals, TransferType.In);
                _dbContext.Add(tx);
            }

            var walletSender = allWallets.FirstOrDefault(s => s.Address == transfer.From);
            if (walletSender != null)
            {
                var tx = CreateErc20Transaction(request, transfer, cmcInfo.CurrentPriceUsd * transfer.ValueWithDecimals, TransferType.Out);
                _dbContext.Add(tx);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task<Erc20Token> CreateErc20Token(AddErc20TransferCommand request, Erc20TransferModel transfer, string tokenAddress, bool isExistsAtCoinMarketCap)
    {
        Erc20Token token = new Erc20Token(tokenAddress, request.ChainId, transfer.TokenName, transfer.TokenSymbol, false, false);

        token.ExistAtCoinmarketCap = isExistsAtCoinMarketCap;

        var explorerApiClient = _chainExplorerApiClientFactory.CreateChainExplorerApiClient(request.ChainId);
        if (explorerApiClient != null)
        {
            var contractVerificationResult = await explorerApiClient.GetContractVerified(tokenAddress);
            token.ContractCodePublished = contractVerificationResult.IsVerified;
        }
        else
        {
            token.ContractCodePublished = false;
        }

        return token;
    }

    private static Erc20Transaction CreateErc20Transaction(
        AddErc20TransferCommand request, 
        Erc20TransferModel transfer,
        decimal? usdValue,
        TransferType transferType)
    {
        var erc20TransactionId = Guid.NewGuid();
        var tx = new Erc20Transaction(
            erc20TransactionId,
            transferType == TransferType.In ? transfer.To : transfer.From,
            transfer.TransactionHash,
            request.At,
            transferType,
            request.ChainId,
            transfer.Contract.ToLower(),
            transfer.Value,
            transfer.ValueWithDecimals,
            usdValue);

        tx.TransactionChain = new Erc20TransactionChain(erc20TransactionId, request.ChainId);
        return tx;
    }
}

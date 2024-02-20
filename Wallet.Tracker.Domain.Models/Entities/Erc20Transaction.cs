namespace Wallet.Tracker.Domain.Models.Entities;

using Wallet.Tracker.Domain.Models.Enums;

public class Erc20Transaction
{
    public Erc20Transaction(
        Guid id,
        string walletAddress,
        string txHash,
        DateTime at,
        TransferType transferType,
        string chainId,
        string tokenAddress,
        string nativeAmount,
        decimal amount,
        decimal? usdValue)
    {
        Id = id;
        WalletAddress = walletAddress;
        TxHash = txHash;
        At = at;
        TransferType = transferType;
        ChainId = chainId;
        TokenAddress = tokenAddress;
        NativeAmount = nativeAmount;
        Amount = amount;
        UsdValue = usdValue;
    }

    public Guid Id { get; set; }
    public string WalletAddress { get; set; }
    public string TxHash { get; set; }
    public DateTime At { get; set; }
    public TransferType TransferType { get; set; }
    public string ChainId { get; set; }
    public string TokenAddress { get; set; }
    public string NativeAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal? UsdValue { get; set; }
    public Erc20TransactionChain TransactionChain { get; set; }
    public Erc20Token Token { get; set; }
}

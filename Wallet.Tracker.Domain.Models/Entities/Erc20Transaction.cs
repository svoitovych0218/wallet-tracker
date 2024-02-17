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
        string symbol,
        string tokenName,
        string nativeValue,
        string contractAddress,
        decimal amount)
    {
        Id = id;
        WalletAddress = walletAddress;
        TxHash = txHash;
        At = at;
        TransferType = transferType;
        Symbol = symbol;
        TokenName = tokenName;
        NativeValue = nativeValue;
        ContractAddress = contractAddress;
        Amount = amount;
    }

    public Guid Id { get; set; }
    public string WalletAddress { get; set; }
    public string TxHash { get; set; }
    public DateTime At { get; set; }
    public TransferType TransferType { get; set; }
    public string Symbol { get; set; }
    public string TokenName { get; set; }
    public string NativeValue { get; set; }
    public string ContractAddress { get; set; }
    public decimal Amount { get; set; }
    
    public Erc20TransactionChain TransactionChain { get; set; }
}

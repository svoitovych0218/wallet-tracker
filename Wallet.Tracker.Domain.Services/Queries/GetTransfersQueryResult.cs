namespace Wallet.Tracker.Domain.Services.Queries;

using Wallet.Tracker.Domain.Models.Enums;

public class TransferData
{
    public string TokenName { get; set; }
    public string TokenSymbol { get; set; }
    public decimal Amount { get; set; }
    public TransferType TransferType { get; set; }
    public string ContractAddress { get; set; }
}
public class TransactionData
{
    public string WalletAddress { get; set; }
    public string TxHash { get; set; }
    public DateTime At { get; set; }
    public IEnumerable<TransferData> Transfers { get; set; }
}

public class GetTransfersQueryResult
{
    public GetTransfersQueryResult(IEnumerable<TransactionData> transactions)
    {
        Transactions = transactions;
    }

    public IEnumerable<TransactionData> Transactions { get; }
}

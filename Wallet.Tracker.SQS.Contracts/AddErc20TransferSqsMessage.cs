namespace Wallet.Tracker.SQS.Contracts;

public class Erc20TransferSqsModel
{
    public string TransactionHash { get; set; }
    public string Contract { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Value { get; set; }
    public string TokenName { get; set; }
    public string TokenSymbol { get; set; }
    public int TokenDecimals { get; set; }
    public decimal ValueWithDecimals { get; set; }
    public bool PossibleSpam { get; set; }
}

public class AddErc20TransferSqsMessage
{
    public bool Confirmed { get; set; }
    public string ChainId { get; set; }
    public DateTime At { get; set; }
    public IEnumerable<Erc20TransferSqsModel> Erc20Transfers { get; set; }
}

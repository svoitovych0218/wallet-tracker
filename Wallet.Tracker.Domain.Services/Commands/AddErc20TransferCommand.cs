namespace Wallet.Tracker.Domain.Services.Commands;

using MediatR;

public class Erc20TransferModel
{
    public Erc20TransferModel(
        string transactionHash,
        string contract,
        string from,
        string to,
        string value,
        string tokenName,
        string tokenSymbol,
        int tokenDecimals,
        decimal valueWithDecimals,
        bool possibleSpam)
    {
        TransactionHash = transactionHash;
        Contract = contract;
        From = from;
        To = to;
        Value = value;
        TokenName = tokenName;
        TokenSymbol = tokenSymbol;
        TokenDecimals = tokenDecimals;
        ValueWithDecimals = valueWithDecimals;
        PossibleSpam = possibleSpam;
    }

    public string TransactionHash { get; }
    public string Contract { get; }
    public string From { get; }
    public string To { get; }
    public string Value { get; }
    public string TokenName { get; }
    public string TokenSymbol { get; }
    public int TokenDecimals { get; }
    public decimal ValueWithDecimals { get; }
    public bool PossibleSpam { get; }
}

public class AddErc20TransferCommand : IRequest<Unit>
{
    public AddErc20TransferCommand(
        bool confirmed,
        string chainId,
        DateTime at,
        IEnumerable<Erc20TransferModel> erc20Transfers)
    {
        Confirmed = confirmed;
        ChainId = chainId;
        At = at;
        Erc20Transfers = erc20Transfers;
    }

    public bool Confirmed { get; }
    public string ChainId { get; }
    public DateTime At { get; }
    public IEnumerable<Erc20TransferModel> Erc20Transfers { get; }
}

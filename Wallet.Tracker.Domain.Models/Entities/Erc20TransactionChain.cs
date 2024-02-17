namespace Wallet.Tracker.Domain.Models.Entities;
public class Erc20TransactionChain
{
    public Erc20TransactionChain(Guid erc20TransactionId, string chainId)
    {
        Erc20TransactionId = erc20TransactionId;
        ChainId = chainId;
    }

    public Guid Erc20TransactionId { get; set; }
    public string ChainId { get; set; }

    public Chain Chain { get; set; }
    public Erc20Transaction Erc20Transaction { get; set; }
}

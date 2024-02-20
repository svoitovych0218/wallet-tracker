namespace Wallet.Tracker.Infrastruction.ChainExplorer.Contracts;
public class ContractVerified
{
    public ContractVerified(bool isVerified)
    {
        IsVerified = isVerified;
    }

    public bool IsVerified { get; }
}

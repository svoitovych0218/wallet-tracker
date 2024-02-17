namespace Wallet.Tracker.Domain.Models.Entities;
public class WalletChain
{
    public WalletChain(string chainId, string walletAddress)
    {
        ChainId = chainId;
        WalletAddress = walletAddress;
    }

    public string ChainId { get; set; }
    public string WalletAddress { get; set; }

    public Chain Chain { get; set; }
    public WalletData Wallet { get; set; }
}

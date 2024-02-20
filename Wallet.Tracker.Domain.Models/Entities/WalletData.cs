namespace Wallet.Tracker.Domain.Models.Entities;
public class WalletData
{
    public WalletData(string address, string title, DateTime createdAt)
    {
        Address = address;
        Title = title;
        CreatedAt = createdAt;
    }
    public string Address { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid MoralisStreamId { get; set; }
    public bool IsDeleted { get; set; }

    public List<WalletChain> TrackingChains { get; set; } = new List<WalletChain>();
    public List<Erc20Transaction> Erc20Transactions { get; set; } = new List<Erc20Transaction>();
}

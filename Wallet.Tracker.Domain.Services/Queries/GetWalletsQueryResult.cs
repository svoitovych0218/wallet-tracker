namespace Wallet.Tracker.Domain.Services.Queries;

public class WalletViewModel
{
    public string Address { get; set; }
    public string Title { get; set; }
    public IEnumerable<string> ChainIds { get; set; }
    public int NotificationsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class GetWalletsQueryResult
{
    public IEnumerable<WalletViewModel> Wallets { get; set; }
}

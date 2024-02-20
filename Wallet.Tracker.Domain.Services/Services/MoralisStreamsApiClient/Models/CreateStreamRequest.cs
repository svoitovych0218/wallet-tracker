namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public class CreateStreamRequest
{
    public CreateStreamRequest(string walletAddress, string[] chainIds, string title)
    {
        WalletAddress = walletAddress;
        ChainIds = chainIds;
        Title = title;
    }

    public string WalletAddress { get; }
    public string[] ChainIds { get; }
    public string Title { get; }
}

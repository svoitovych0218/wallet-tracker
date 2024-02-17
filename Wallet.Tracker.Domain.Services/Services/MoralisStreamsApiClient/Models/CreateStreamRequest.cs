namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public class CreateStreamRequest
{
    public CreateStreamRequest(string walletAddress, string chainId)
    {
        WalletAddress = walletAddress;
        ChainId = chainId;
    }

    public string WalletAddress { get; }
    public string ChainId { get; }
}

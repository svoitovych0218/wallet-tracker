namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public interface IMoralisStreamsApiClient
{
    Task<Guid> CreateStream(CreateStreamRequest request);
}

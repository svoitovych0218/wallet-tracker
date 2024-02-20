namespace Wallet.Tracker.Domain.Services.Services.Interfaces;
public interface IChainExplorerApiClientFactory
{
    IChainExplorerApiClient? CreateChainExplorerApiClient(string chainId);
}

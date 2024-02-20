namespace Wallet.Tracker.Infrastruction.ChainExplorer.Arbitrum;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class ArbiScanApiClient : ChainExplorerApiClientBase
{
    public ArbiScanApiClient(
        ILogger<ArbiScanApiClient> logger,
        IOptions<ArbitrumOptions> options)
        : base("https://api.arbiscan.io/", options.Value.ArbitrumScanApiKey, logger) { }
}

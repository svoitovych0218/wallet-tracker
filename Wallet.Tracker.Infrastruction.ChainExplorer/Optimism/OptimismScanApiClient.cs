namespace Wallet.Tracker.Infrastruction.ChainExplorer.Optimism;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class OptimismScanApiClient : ChainExplorerApiClientBase
{
    public OptimismScanApiClient(
        ILogger<OptimismScanApiClient> logger,
        IOptions<OptimismOptions> options)
        : base("https://api-optimistic.etherscan.io/", options.Value.OptimismScanApiKey, logger) { }
}

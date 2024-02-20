namespace Wallet.Tracker.Infrastruction.ChainExplorer.Bsc;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class BscScanApiClient : ChainExplorerApiClientBase
{
    public BscScanApiClient(
        ILogger<BscScanApiClient> logger,
        IOptions<BscOptions> options)
        : base("https://api.bscscan.com/", options.Value.BscScanApiKey, logger) { }
}

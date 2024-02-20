namespace Wallet.Tracker.Infrastruction.ChainExplorer.Fantom;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class FantomScanApiClient : ChainExplorerApiClientBase
{
    public FantomScanApiClient(
        ILogger<FantomScanApiClient> logger,
        IOptions<FantomOptions> options)
        : base("https://api.ftmscan.com/", options.Value.FantomScanApiKey, logger) { }
}

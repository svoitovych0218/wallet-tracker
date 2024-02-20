namespace Wallet.Tracker.Infrastruction.ChainExplorer.Base;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class BaseScanApiClient : ChainExplorerApiClientBase
{
    public BaseScanApiClient(
        ILogger<ChainExplorerApiClientBase> logger,
        IOptions<BaseOptions> options)
        : base("https://api.basescan.org/", options.Value.BaseScanApiKey, logger) { }
}

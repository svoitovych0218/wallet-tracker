namespace Wallet.Tracker.Infrastruction.ChainExplorer.Cronos;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class CronosScanApiClient : ChainExplorerApiClientBase
{
    public CronosScanApiClient(
        ILogger<CronosScanApiClient> logger,
        IOptions<CronosOptions> options)
        : base("https://api.cronoscan.com/", options.Value.CronosScanApiKey, logger) { }
}

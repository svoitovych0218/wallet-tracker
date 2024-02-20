namespace Wallet.Tracker.Infrastruction.ChainExplorer.Polygon;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class PolygonScanApiClient : ChainExplorerApiClientBase
{
    public PolygonScanApiClient(
        ILogger<PolygonScanApiClient> logger,
        IOptions<PolygonOptions> options)
        : base("https://api.polygonscan.com/", options.Value.PolygonScanApiKey, logger) { }
}

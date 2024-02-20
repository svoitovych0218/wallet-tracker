namespace Wallet.Tracker.Infrastruction.ChainExplorer.Ether;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Options;

internal class EtherScanApiClient : ChainExplorerApiClientBase
{
    public EtherScanApiClient(
        ILogger<EtherScanApiClient> logger,
        IOptions<EtherOptions> options)
        : base("https://api.etherscan.io/", options.Value.EtherScanApiKey, logger) { }
}

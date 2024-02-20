namespace Wallet.Tracker.Infrastruction.ChainExplorer.Factory;
using System;
using Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;
using Wallet.Tracker.Infrastruction.ChainExplorer.Arbitrum;
using Wallet.Tracker.Infrastruction.ChainExplorer.Base;
using Wallet.Tracker.Infrastruction.ChainExplorer.Bsc;
using Wallet.Tracker.Infrastruction.ChainExplorer.Cronos;
using Wallet.Tracker.Infrastruction.ChainExplorer.Ether;
using Wallet.Tracker.Infrastruction.ChainExplorer.Fantom;
using Wallet.Tracker.Infrastruction.ChainExplorer.Optimism;
using Wallet.Tracker.Infrastruction.ChainExplorer.Polygon;

internal class ChainExplorerApiClientFactory : IChainExplorerApiClientFactory
{
    private readonly Dictionary<string, ChainExplorerApiClientBase> _clients;

    public ChainExplorerApiClientFactory(
        ArbiScanApiClient arbitrumScanApiClient,
        BscScanApiClient bscScanApiClient,
        CronosScanApiClient cronosScanApiClient,
        EtherScanApiClient etherScanApiClient,
        FantomScanApiClient fantomScanApiClient,
        OptimismScanApiClient optimismScanApiClient,
        PolygonScanApiClient polygonScanApiClient,
        BaseScanApiClient baseScanApiClient
        )
    {
        _clients = new Dictionary<string, ChainExplorerApiClientBase>
        {
            { "0xa4b1", arbitrumScanApiClient },
            { "0x38", bscScanApiClient },
            { "0x19", cronosScanApiClient },
            { "0x1", etherScanApiClient },
            { "0xfa", fantomScanApiClient },
            { "0xa", optimismScanApiClient },
            { "0x89", polygonScanApiClient },
            { "0x2105", baseScanApiClient }
        };
    }

    public IChainExplorerApiClient? CreateChainExplorerApiClient(string chainId)
    {
        _clients.TryGetValue(chainId, out ChainExplorerApiClientBase? client);
        return client;
    }
}

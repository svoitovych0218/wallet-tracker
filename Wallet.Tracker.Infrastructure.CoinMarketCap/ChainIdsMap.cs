namespace Wallet.Tracker.Infrastructure.CoinMarketCap;

public static class ChainIdsMap
{
    private static readonly Dictionary<int, string> CoinMarketCapIdInternalId
        = new Dictionary<int, string>()
        {
            { 1027, "0x1" }, // ETH
            { 1839, "0x38" }, // BSC
            { 3513, "0xfa" }, // Fantom
            { 3890, "0x89" }, // Polygon
            { 5805, "0xa86a" }, // Avalanche
            { 11840, "0xa" }, // Optimism
            { 11841, "0xa4b1" }, // Arbitrum
            { 3635, "0x19" }, //Cronos
            { 11145, "0x171" }, // PulseChain
            { 1659, "0x64" }, // Gnosis
        };

    public static string? GetInvernalChainId(int coinMarketCapChainId)
    {
        CoinMarketCapIdInternalId.TryGetValue(coinMarketCapChainId, out var id);

        return id;
    }
}

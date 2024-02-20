namespace Wallet.Tracker.Infrastructure.CoinMarketCap.Contracts;
public class CoinMarketCapTokenInfoResponse
{
    public CoinMarketCapTokenInfoResponse(
        bool isExists,
        decimal? currentPrice)
    {
        IsExists = isExists;
        CurrentPriceUsd = currentPrice;
    }

    public bool IsExists { get; }
    public decimal? CurrentPriceUsd { get; }
}

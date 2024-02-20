namespace Wallet.Tracker.Infrastructure.CoinMarketCap.ResponseModels;

internal class QuoteDataModel 
{
    public decimal Price { get; set; }
}

internal class PriceDataModel
{
    public Dictionary<string, QuoteDataModel> Quote { get; set; }
}

internal class CoinMarketCapApiPriceResponse
{
    public CoinMarketCapApiResponseStatusModel Status { get; set; }

    public Dictionary<string, PriceDataModel> Data { get; set; }
}

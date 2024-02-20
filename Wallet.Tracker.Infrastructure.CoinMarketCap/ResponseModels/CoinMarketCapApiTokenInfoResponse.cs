namespace Wallet.Tracker.Infrastructure.CoinMarketCap.ResponseModels;

using System.Text.Json.Serialization;

public class CoinMarketCapApiTokenPlatformModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}

public class CoinMarketCapApiTokenContractAddressPlatformCoin
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}

public class CoinMarketCapApiTokenContractAddressPlatform
{
    public string Name { get; set; }

    public CoinMarketCapApiTokenContractAddressPlatformCoin Coin { get; set; }
}

public class CoinMarketCapApiTokenContractAddress
{
    [JsonPropertyName("contract_address")]
    public string ContractAddress { get; set; }
    public CoinMarketCapApiTokenContractAddressPlatform Platform { get; set; }

}


public class CoinMarketCapApiTokenDataModel
{
    public CoinMarketCapApiTokenPlatformModel Platform { get; set; }

    [JsonPropertyName("contract_address")]
    public CoinMarketCapApiTokenContractAddress[] ContractAddresses { get; set; }

}

internal class CoinMarketCapApiTokenInfoResponse
{
    public CoinMarketCapApiResponseStatusModel Status { get; set; }

    public Dictionary<string, CoinMarketCapApiTokenDataModel> Data { get; set; }

}

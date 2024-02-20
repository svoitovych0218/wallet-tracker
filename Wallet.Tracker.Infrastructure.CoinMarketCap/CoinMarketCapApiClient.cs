namespace Wallet.Tracker.Infrastructure.CoinMarketCap;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastructure.CoinMarketCap.Contracts;
using Wallet.Tracker.Infrastructure.CoinMarketCap.Options;
using Wallet.Tracker.Infrastructure.CoinMarketCap.ResponseModels;

internal class CoinMarketCapApiClient : ICoinMerketCapApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CoinMarketCapApiClient> _logger;

    public CoinMarketCapApiClient(IOptions<CoinMarketCapApiOptions> options, ILogger<CoinMarketCapApiClient> logger)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri("https://pro-api.coinmarketcap.com/") };
        _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", options.Value.ApiKey);
        _logger = logger;
    }

    public async Task<CoinMarketCapTokenInfoResponse> GetTokenInfo(string tokenAddress)
    {
        var checksumAddress = new Nethereum.Util.AddressUtil().ConvertToChecksumAddress(tokenAddress);
        _logger.LogInformation($"Request information from CoinMarketCap for address: {checksumAddress}");

        var tokenInfoResponse = await _httpClient.GetAsync($"/v2/cryptocurrency/info?address={checksumAddress}");
        if (!tokenInfoResponse.IsSuccessStatusCode)
        {
            return new CoinMarketCapTokenInfoResponse(false, null);
        }
        
        var body = await tokenInfoResponse.Content.ReadAsStringAsync();
        var tokenInfoResult = JsonConvert.DeserializeObject<CoinMarketCapApiTokenInfoResponse>(body);

        if (tokenInfoResult == null || tokenInfoResult.Data == null)
        {
            return new CoinMarketCapTokenInfoResponse(false, null);
        }

        var keys = tokenInfoResult.Data.Keys;

        if (keys.Count != 1)
        {
            return new CoinMarketCapTokenInfoResponse(true, null);
        }

        var currencyId = keys.First();
        var priceResponse = await _httpClient.GetAsync($"/v1/cryptocurrency/quotes/latest?id={currencyId}");
        if (!priceResponse.IsSuccessStatusCode)
        {
            return new CoinMarketCapTokenInfoResponse(true, null);
        }
        
        var priceBody = await priceResponse.Content.ReadAsStringAsync();
        var priceInfoResult = JsonConvert.DeserializeObject<CoinMarketCapApiPriceResponse>(priceBody);

        if (priceInfoResult == null || priceInfoResult.Data == null)
        {
            return new CoinMarketCapTokenInfoResponse(true, null);
        }

        priceInfoResult.Data.TryGetValue(currencyId, out var priceDataModel);

        if (priceDataModel == null || priceDataModel.Quote == null)
        {
            return new CoinMarketCapTokenInfoResponse(true, null);
        }

        priceDataModel.Quote.TryGetValue("USD", out var priceData);

        return new CoinMarketCapTokenInfoResponse(true, priceData?.Price);
    }
}

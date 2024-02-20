namespace Wallet.Tracker.Infrastruction.ChainExplorer.Abstract;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastruction.ChainExplorer.Contracts;
using Wallet.Tracker.Infrastruction.ChainExplorer.Models.Common;

internal abstract class ChainExplorerApiClientBase : IChainExplorerApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<ChainExplorerApiClientBase> _logger;

    public ChainExplorerApiClientBase(
        string baseUrl,
        string apiKey,
        ILogger<ChainExplorerApiClientBase> logger)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };
        _apiKey = apiKey;
        _logger = logger;
    }


    public async Task<ContractVerified> GetContractVerified(string address)
    {
        try
        {
            _logger.LogInformation($"{nameof(ChainExplorerApiClientBase)}. Send request to contract verification info.");
            var result = await CallExplorerApiToGetContractVerificationInfo(address);
            _logger.LogInformation($"Request finished successfully. {address} isVerified: {result.IsVerified}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during request contract verification info.");
            return new ContractVerified(false);
        }
    }

    protected virtual async Task<ContractVerified> CallExplorerApiToGetContractVerificationInfo(string address)
    {

        var res = await _httpClient.GetAsync($"/api?module=contract&action=getabi&address={address}&apikey={_apiKey}");
        if (!res.IsSuccessStatusCode)
        {
            return new ContractVerified(false);
        }

        var body = await res.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ContractVerifiedApiResponse>(body);

        return new ContractVerified(result != null && result.Status == "1" && result.Message == "OK");
    }
}

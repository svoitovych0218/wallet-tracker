namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient;

using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

public class MoralisStreamsApiClient : IMoralisStreamsApiClient
{
    private readonly ILogger<MoralisStreamsApiClient> _logger;
    private HttpClient _httpClient;

    public MoralisStreamsApiClient(
        IOptions<MoralisApiKeyOption> options,
        ILogger<MoralisStreamsApiClient> logger)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.moralis-streams.com/"),
        };
        logger.LogInformation("ApiKey: " + options.Value.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", options.Value.ApiKey);
        _logger = logger;
    }

    public async Task<Guid> CreateStream(CreateStreamRequest request)
    {
        var streamId = await AddStreamViaApi(request.ChainId, request.WalletAddress);

        return streamId;
    }

    public async Task<Guid> AddStreamViaApi(string chainId, string address)
    {
        var apiRequest = new CreateErc20TransferStreamApiRequest()
        {
            WebhookUrl = "https://7zhz455zj6.execute-api.eu-central-1.amazonaws.com/Prod/api/webhook/erc-transfer",
            Description = "ApiGenerated",
            Tag = "CommandTest",
            ChainIds = new string[] { chainId }
        };

        var body = JsonConvert.SerializeObject(apiRequest, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        });

        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync("/streams/evm", content);
        _logger.LogInformation("Request to add stream sent. Body: " + body);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<CreateStreamApiResponse>(responseBody);

        var addAddressRequestModel = new AddAddressToStreamApiRequest(address);
        var addAddressRequestBody = JsonConvert.SerializeObject(addAddressRequestModel, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        });
        var addAddressContent = new StringContent(addAddressRequestBody, Encoding.UTF8, "application/json");
        var addAddressResponse = await _httpClient.PostAsync($"/streams/evm/{apiResponse.Id}/address", addAddressContent);
        _logger.LogInformation("Request to add address sent. Body: " + addAddressRequestBody);
        addAddressResponse.EnsureSuccessStatusCode();

        return apiResponse.Id;
    }

}

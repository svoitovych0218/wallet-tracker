namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

using Newtonsoft.Json;

internal class AbiInputApi2Request : AbiInputApiRequestBase
{
    [JsonProperty("abi.0.inputs.2.name")]
    public string Abi0Inputs2Name { get; set; }

    [JsonProperty("abi.0.inputs.2.type")]
    public string Abi0Inputs2Type { get; set; }
}

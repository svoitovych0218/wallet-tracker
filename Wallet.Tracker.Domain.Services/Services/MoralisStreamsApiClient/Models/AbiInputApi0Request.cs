namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

using Newtonsoft.Json;

internal class AbiInputApi0Request : AbiInputApiRequestBase
{
    [JsonProperty("abi.0.inputs.0.name")]
    public string Abi0Inputs0Name { get; set; }

    [JsonProperty("abi.0.inputs.0.type")]
    public string Abi0Inputs0Type { get; set; }
}

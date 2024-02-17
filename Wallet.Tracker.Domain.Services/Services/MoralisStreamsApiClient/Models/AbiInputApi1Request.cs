namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

using Newtonsoft.Json;

internal class AbiInputApi1Request : AbiInputApiRequestBase
{
    [JsonProperty("abi.0.inputs.1.name")]
    public string Abi0Inputs1Name { get; set; }

    [JsonProperty("abi.0.inputs.1.type")]
    public string Abi0Inputs1Type { get; set; }
}

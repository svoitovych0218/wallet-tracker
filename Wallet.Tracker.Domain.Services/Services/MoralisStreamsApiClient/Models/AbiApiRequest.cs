namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

internal class AbiApiRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public bool Anonymous { get; set; }
    public List<AbiInputApiRequestBase> Inputs { get; set; }

    [JsonProperty("abi.0.type")]
    public string Abi0Type { get; set; }

    [JsonProperty("abi.0.name")]
    public string Abi0Name { get; set; }

    public static AbiApiRequest[] GetErc20TransferAbi()
    {
        var erc20TransferAbi = new AbiApiRequest
        {
            Name = "Transfer",
            Type = "event",
            Anonymous = false,
            Inputs = new List<AbiInputApiRequestBase>
            {
                new AbiInputApi0Request
                {
                    Type = "address",
                    Name = "from",
                    Indexed = true,
                    Abi0Inputs0Name = "from",
                    Abi0Inputs0Type = "address"
                },
                new AbiInputApi1Request
                {
                    Type = "address",
                    Name = "to",
                    Indexed = true,
                    Abi0Inputs1Name = "to",
                    Abi0Inputs1Type = "address"
                },
                new AbiInputApi2Request
                {
                    Type = "uint256",
                    Name = "value",
                    Indexed = false,
                    Abi0Inputs2Name = "value",
                    Abi0Inputs2Type = "uint256"
                }
            },
            Abi0Type = "event",
            Abi0Name = "Transfer"
        };

        return new[] { erc20TransferAbi };
    }
}

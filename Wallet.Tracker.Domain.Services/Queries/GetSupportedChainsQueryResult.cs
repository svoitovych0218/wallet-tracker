namespace Wallet.Tracker.Domain.Services.Queries;
using System.Collections.Generic;

public class ChainData
{
    public ChainData(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }
}

public class GetSupportedChainsQueryResult
{
    public GetSupportedChainsQueryResult(IEnumerable<ChainData> chains) 
    {
        Chains = chains;
    }

    public IEnumerable<ChainData> Chains { get; }
}

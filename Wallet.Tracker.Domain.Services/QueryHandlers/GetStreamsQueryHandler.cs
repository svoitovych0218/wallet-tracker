namespace Wallet.Tracker.Domain.Services.QueryHandlers;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Services.Queries;

public class GetStreamsQueryHandler : IRequestHandler<GetStreamsQuery, GetStreamsQueryResult>
{
    public async Task<GetStreamsQueryResult> Handle(GetStreamsQuery request, CancellationToken cancellationToken)
    {
        Moralis.StreamsApi.MoralisStreamsApiClient.Initialize(null, "4Ts6Nxu5xFFGwdbcGBx9XK8sJ54zQtxGmNvpappHjmyf6eb3FSdeAxZk8liL8nuZ");
        var moralisClient = Moralis.StreamsApi.MoralisStreamsApiClient.StreamsApiClient;
        var response = await moralisClient.StreamsEndpoint.GetStreams(5, null);
        return new GetStreamsQueryResult() { Body = JsonConvert.SerializeObject(response) };
    }
}

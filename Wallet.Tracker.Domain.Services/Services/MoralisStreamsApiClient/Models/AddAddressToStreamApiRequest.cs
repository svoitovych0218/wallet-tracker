namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;
public class AddAddressToStreamApiRequest
{
    public AddAddressToStreamApiRequest(string address)
    {
        Address = address;
    }

    public string Address { get; }
}

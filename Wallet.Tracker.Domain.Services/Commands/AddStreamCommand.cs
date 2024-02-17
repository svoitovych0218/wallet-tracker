namespace Wallet.Tracker.Domain.Services.Commands;

using MediatR;

public class AddStreamCommand : IRequest<Unit>
{
    public AddStreamCommand(string address, string title, string chainId)
    {
        Address = address;
        Title = title;
        ChainId = chainId;
    }

    public string Address { get; }
    public string Title { get; }
    public string ChainId { get; }
}

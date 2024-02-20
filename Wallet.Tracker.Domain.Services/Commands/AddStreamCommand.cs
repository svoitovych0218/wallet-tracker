namespace Wallet.Tracker.Domain.Services.Commands;

using MediatR;

public class AddStreamCommand : IRequest<Unit>
{
    public AddStreamCommand(string address, string title, string[] chainIds)
    {
        Address = address;
        Title = title;
        ChainIds = chainIds;
    }

    public string Address { get; }
    public string Title { get; }
    public string[] ChainIds { get; }
}

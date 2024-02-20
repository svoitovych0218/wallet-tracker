namespace Wallet.Tracker.Domain.Services.Commands;

using MediatR;

public class DeleteStreamCommand : IRequest<Unit>
{
    public DeleteStreamCommand(string address)
    {
        Address = address;
    }

    public string Address { get; }
}

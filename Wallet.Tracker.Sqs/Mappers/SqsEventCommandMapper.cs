namespace Wallet.Tracker.SQS.Mappers;

using MediatR;
using System.Linq;
using Wallet.Tracker.Domain.Services.Commands;
using Wallet.Tracker.SQS.Contracts;

public static class SqsEventCommandMapper
{
    public static IRequest<Unit> MapToAddErc20TransferCommand(AddErc20TransferSqsMessage @event)
    {
        var command = new AddErc20TransferCommand(
            @event.Confirmed,
            @event.ChainId,
            @event.At,
            @event.Erc20Transfers.Select(s => new Erc20TransferModel(
                s.TransactionHash,
                s.Contract,
                s.From,
                s.To,
                s.Value,
                s.TokenName,
                s.TokenSymbol,
                s.TokenDecimals,
                s.ValueWithDecimals,
                s.PossibleSpam
                )));

        return command;
    }
}

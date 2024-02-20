namespace Wallet.Tracker.Api.Services;

using Wallet.Tracker.SQS.Contracts;

public interface ISqsClient
{
    Task SendErc20TransferMessage(AddErc20TransferSqsMessage message);
}

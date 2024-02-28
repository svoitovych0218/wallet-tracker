using Amazon.Lambda.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wallet.Tracker.Sqs.Extensions;
using Wallet.Tracker.Sqs.Handlers;
using Wallet.Tracker.SQS.Contracts;
using Wallet.Tracker.SQS.Mappers;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Wallet.Tracker.SQS.Functions;
public class AddErc20TransferFunction : SqsEventHandlerBase<AddErc20TransferSqsMessage>
{
    public AddErc20TransferFunction()
    {
    }

    public override async Task ProcessSqsMessage(
        AddErc20TransferSqsMessage message,
        ILambdaContext lambdaContext,
        IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<AddErc20TransferFunction>>();
        logger.LogInformation($"{nameof(AddErc20TransferFunction)} {nameof(ProcessSqsMessage)} started");
        var command = SqsEventCommandMapper.MapToAddErc20TransferCommand(message);
        using var cts = lambdaContext.GetCancellationTokenSource();

        var mediator = serviceProvider.GetRequiredService<IMediator>();

        await mediator.Send(command, cts.Token);
        logger.LogInformation($"{nameof(AddErc20TransferFunction)} {nameof(ProcessSqsMessage)} finished");
    }
}
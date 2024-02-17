using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Wallet.Tracker.Sqs.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wallet.Tracker.Sqs.Handlers;

public abstract class SqsEventHandlerBase<TMessage> where TMessage : class, new()
{
    protected readonly IServiceProvider ServiceProvider;
    private List<SQSBatchResponse.BatchItemFailure> _batchItemFailures;
    private readonly SQSBatchResponse _sqsBatchResponse;

    protected SqsEventHandlerBase()
    {
        _sqsBatchResponse = new SQSBatchResponse();
    }

    public abstract Task ProcessSqsMessage(TMessage message, ILambdaContext lambdaContext, IServiceProvider serviceProvider);

    public async Task<SQSBatchResponse> Handler(SQSEvent sqsEvent, ILambdaContext lambdaContext)
    {
        using (var sp = Startup.ConfigureServices().BuildServiceProvider().CreateScope())
        {
            var logger = sp.ServiceProvider.GetRequiredService<ILogger<SqsEventHandlerBase<TMessage>>>();
            using (logger.BeginScope("{RequestId}", lambdaContext.AwsRequestId))
            {
                logger.LogInformation($"{nameof(SqsEventHandlerBase<TMessage>)} started.");
                await ProcessEvent(sqsEvent, lambdaContext, sp.ServiceProvider);

                // Set BatchItemFailures if any
                if (_batchItemFailures != null)
                {
                    logger.LogInformation($"Failed Messages Count: {_batchItemFailures.Count}. Ids: " +
                        $"{string.Join(", ", _batchItemFailures.Select(s => s.ItemIdentifier))}");

                    _sqsBatchResponse.BatchItemFailures = _batchItemFailures;
                }
            }
        }
        return _sqsBatchResponse;
    }

    private async Task ProcessEvent(SQSEvent sqsEvent, ILambdaContext lambdaContext, IServiceProvider serviceProvider)
    {
        var sqsMessages = sqsEvent.Records;
        var batchItemFailures = new List<SQSBatchResponse.BatchItemFailure>();
        var logger = serviceProvider.GetRequiredService<ILogger<SqsEventHandlerBase<TMessage>>>();
        logger.LogInformation($"ProcessEvent. Batch count: {sqsMessages.Count}. MessageIds: {string.Join(", ", sqsMessages.Select(s => s.MessageId))}");

        foreach (var sqsMessage in sqsMessages)
        {
            try
            {
                logger.LogInformation($"MessageId: {sqsMessage.MessageId}. MessageBody: {sqsMessage.Body}");

                var message = JsonConvert.DeserializeObject<TMessage>(
                    sqsMessage.Body,
                    new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Error,
                        ContractResolver = new NonNullablePropertiesRequiredResolver()
                    });

                // This abstract method is implemented by the concrete classes i.e. ProcessEmployeeFunction.
                await ProcessSqsMessage(message, lambdaContext, serviceProvider);
            }
            catch (Exception ex)
            {
                logger.LogError(exception: ex, message: $"{typeof(TMessage).Name} event processing error: " + ex.Message);
                batchItemFailures.Add(
                    new SQSBatchResponse.BatchItemFailure
                    {
                        ItemIdentifier = sqsMessage.MessageId
                    }
                );
            }
        }

        _batchItemFailures = batchItemFailures;
    }
}
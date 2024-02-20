namespace Wallet.Tracker.Api.Services;

using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Wallet.Tracker.Api.Extensions;
using Wallet.Tracker.SQS.Contracts;
public class SqsClient : ISqsClient
{
    private readonly AmazonSQSClient _client;
    private readonly string _erc20TransferQueueUrl;
    private readonly ILogger<SqsClient> _logger;

    public SqsClient(ILogger<SqsClient> logger)
    {
        _client = new AmazonSQSClient(Amazon.RegionEndpoint.EUCentral1);
        _logger = logger;
        _erc20TransferQueueUrl = $"https://sqs." +
        $"{Environment.GetEnvironmentVariable("Region")}.amazonaws.com/" +
        $"{Environment.GetEnvironmentVariable("AWSAccountId")}/" +
        $"{Environment.GetEnvironmentVariable("CurrentEnvironment")}-evm-erc20-transfer-queue.fifo";

    }

    public async Task SendErc20TransferMessage(AddErc20TransferSqsMessage message)
    {
        _logger.LogInformation("Start sending message to queue url: " + _erc20TransferQueueUrl);

        var body = JsonConvert.SerializeObject(message);

        _logger.LogInformation("Message Body: " + body);

        await SendMessage(body, _erc20TransferQueueUrl);

        _logger.LogInformation("Message sent successfully");
    }

    private async Task SendMessage(string body, string queue)
    {
        var deduplicationId = body.GetStringSha256Hash();
        _logger.LogInformation("DeduplicationId: " + deduplicationId);

        await _client.SendMessageAsync(new SendMessageRequest(queue, body) 
        {
            MessageGroupId = "Erc20Transfer",
            MessageDeduplicationId = deduplicationId
        });
    }
}

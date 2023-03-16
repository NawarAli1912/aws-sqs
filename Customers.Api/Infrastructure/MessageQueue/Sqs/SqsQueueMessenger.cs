using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.Api.Infrastructure.MessageQueue.Messages;
using Microsoft.Extensions.Options;

namespace Customers.Api.Infrastructure.MessageQueue.Sqs;

public class SqsQueueMessenger : IQueueMessenger
{
    private readonly IAmazonSQS _sqs;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly ILogger<SqsQueueMessenger> _logger;
    private string? _queueUrl;

    public SqsQueueMessenger(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings, ILogger<SqsQueueMessenger> logger)
    {
        _sqs = sqs;
        _queueSettings = queueSettings;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T message) where T : IQueueMessage
    {
        _queueUrl = await GetQueueUrlAsync();

        var sendMessageRequest = new SendMessageRequest
        {
            MessageBody = message.JsonMessage,
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MesssageType",
                    new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            },
            QueueUrl = _queueUrl
        };

        try
        {
            await _sqs.SendMessageAsync(sendMessageRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to send a queue message", ex.Message);
        }
    }

    private async Task<string> GetQueueUrlAsync()
    {
        if (_queueUrl is not null)
            return _queueUrl;
        var response = await _sqs.GetQueueUrlAsync(_queueSettings.Value.QueueName);
        _queueUrl = response.QueueUrl;
        return _queueUrl;
    }
}

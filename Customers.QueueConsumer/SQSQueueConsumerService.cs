using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.QueueConsumer.Messages;
using MediatR;
using Microsoft.Extensions.Options;

namespace Customers.QueueConsumer;

public class SQSQueueConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqs;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly ISender _mediator;
    private readonly ILogger<SQSQueueConsumerService> _logger;

    public SQSQueueConsumerService(IAmazonSQS sqs, IOptions<QueueSettings> queueSettings, ISender mediator, ILogger<SQSQueueConsumerService> logger)
    {
        _sqs = sqs;
        _queueSettings = queueSettings;
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrlResponse = await _sqs.GetQueueUrlAsync(_queueSettings.Value.QueueName, stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string> { "All" },
            MaxNumberOfMessages = 1
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

            foreach (var message in response.Messages)
            {
                var messageType = message.MessageAttributes.GetValueOrDefault(_queueSettings.Value.MessageAttributes.Type);

                if (messageType is null)
                {
                    _logger.LogWarning("No message type was included in the message");

                    continue;
                }

                // retrieving the Type object dynamically at runtime
                var type = Type.GetType($"Customers.QueueConsumer.Messages.{messageType.StringValue}");

                if (type is null)
                {
                    _logger.LogWarning("Unknown Message Type: The messge type is not in the contract {MessageType}.", messageType);
                    // we wont delete the message in this case
                    continue;
                }

                var typedMessage = (ISqsReceviedMessage)JsonSerializer.Deserialize(message.Body, type)!;

                try
                {
                    await _mediator.Send(typedMessage, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Message Failed during processing : {message.MessageId}");
                    // we wont delete the message in this case
                    continue;
                }


                await _sqs.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
            }


            await Task.Delay(1000, stoppingToken);
        }
    }
}

namespace Customers.QueueConsumer.Messages;

public class QueueSettings
{
    public required string QueueName { get; set; }

    public required MessageAttributes MessageAttributes { get; set; }
}

public class MessageAttributes
{
    public required string Type { get; set; }
}



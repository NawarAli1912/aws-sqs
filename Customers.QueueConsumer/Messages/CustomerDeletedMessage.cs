namespace Customers.QueueConsumer.Messages;

public class CustomerDeletedMessage : ISqsReceviedMessage
{
    public Guid Id { get; set; }
}

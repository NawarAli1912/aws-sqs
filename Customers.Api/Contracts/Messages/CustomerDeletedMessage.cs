using Customers.Api.Infrastructure.MessageQueue.Messages;

namespace Customers.Api.Contracts.Messages;

public class CustomerDeletedMessage : IQueueMessage
{
    public Guid Id { get; set; }
}

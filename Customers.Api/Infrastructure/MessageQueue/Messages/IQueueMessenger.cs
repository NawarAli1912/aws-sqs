namespace Customers.Api.Infrastructure.MessageQueue.Messages
{
    public interface IQueueMessenger
    {
        Task PublishAsync<T>(T message) where T : IQueueMessage;
    }
}

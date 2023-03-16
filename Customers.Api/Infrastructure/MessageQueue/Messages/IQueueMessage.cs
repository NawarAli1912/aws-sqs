using Newtonsoft.Json;

namespace Customers.Api.Infrastructure.MessageQueue.Messages;

public interface IQueueMessage
{
    public string JsonMessage => JsonConvert.SerializeObject(this);
}

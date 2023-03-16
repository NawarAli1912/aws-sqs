using Customers.Api.Infrastructure.MessageQueue.Messages;

namespace Customers.Api.Contracts.Messages;

public class CustomerUpdatedMessage : IQueueMessage
{
    public required Guid Id { get; set; }

    public required string GitHubUsername { get; set; }

    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public required DateTime DateOfBirth { get; set; }
}

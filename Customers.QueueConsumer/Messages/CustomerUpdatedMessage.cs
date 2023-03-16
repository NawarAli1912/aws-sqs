namespace Customers.QueueConsumer.Messages;

public class CustomerUpdatedMessage : ISqsReceviedMessage
{
    public required Guid Id { get; set; }

    public required string GitHubUsername { get; set; }

    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public required DateTime DateOfBirth { get; set; }
}

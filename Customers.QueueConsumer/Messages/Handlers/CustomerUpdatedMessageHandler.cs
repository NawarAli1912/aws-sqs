using MediatR;

namespace Customers.QueueConsumer.Messages.Handlers;

public class CustomerUpdatedMessageHandler : IRequestHandler<CustomerUpdatedMessage>
{
    private readonly ILogger<CustomerUpdatedMessageHandler> _logger;

    public CustomerUpdatedMessageHandler(ILogger<CustomerUpdatedMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerUpdatedMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"The customer with {request.Id} id, {request.Fullname}/{request.GitHubUsername} was updated.");

        return Unit.Task;
    }
}

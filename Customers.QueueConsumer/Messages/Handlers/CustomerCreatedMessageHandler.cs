using MediatR;

namespace Customers.QueueConsumer.Messages.Handlers;

public class CustomerCreatedMessageHandler : IRequestHandler<CustomerCreatedMessage>
{
    private readonly ILogger<CustomerCreatedMessageHandler> _logger;

    public CustomerCreatedMessageHandler(ILogger<CustomerCreatedMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCreatedMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"The customer with {request.Id} id, {request.Fullname}/{request.GitHubUsername} was created.");

        return Unit.Task;
    }
}

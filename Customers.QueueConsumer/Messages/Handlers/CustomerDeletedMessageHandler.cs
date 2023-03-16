using MediatR;

namespace Customers.QueueConsumer.Messages.Handlers;

public class CustomerDeletedMessageHandler : IRequestHandler<CustomerDeletedMessage>
{
    private readonly ILogger<CustomerDeletedMessageHandler> _logger;

    public CustomerDeletedMessageHandler(ILogger<CustomerDeletedMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerDeletedMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"The customer with {request.Id} id was deleted.");

        return Unit.Task;
    }
}

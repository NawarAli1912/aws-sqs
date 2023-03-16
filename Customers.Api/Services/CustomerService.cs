using Customers.Api.Contracts.Messages;
using Customers.Api.Domain;
using Customers.Api.Infrastructure.MessageQueue.Messages;
using Customers.Api.Mapping;
using Customers.Api.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Customers.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IGitHubService _gitHubService;
    private readonly IQueueMessenger _queueMessenger;

    public CustomerService(ICustomerRepository customerRepository,
        IGitHubService gitHubService,
        IQueueMessenger queueMessenger)
    {
        _customerRepository = customerRepository;
        _gitHubService = gitHubService;
        _queueMessenger = queueMessenger;
    }

    public async Task<bool> CreateAsync(Customer customer)
    {
        var existingUser = await _customerRepository.GetAsync(customer.Id);
        if (existingUser is not null)
        {
            var message = $"A user with id {customer.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(nameof(Customer), message));
        }

        var isValidGitHubUser = await _gitHubService.IsValidGitHubUser(customer.GitHubUsername);
        if (!isValidGitHubUser)
        {
            var message = $"There is no GitHub user with username {customer.GitHubUsername}";
            throw new ValidationException(message, GenerateValidationError(nameof(customer.GitHubUsername), message));
        }

        var customerDto = customer.ToCustomerDto();
        var result = await _customerRepository.CreateAsync(customerDto);
        if (result)
        {
            await _queueMessenger.PublishAsync(customer.ToCustomerCreatedMessage());
        }

        return result;
    }

    public async Task<Customer?> GetAsync(Guid id)
    {
        var customerDto = await _customerRepository.GetAsync(id);
        return customerDto?.ToCustomer();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var customerDtos = await _customerRepository.GetAllAsync();
        return customerDtos.Select(x => x.ToCustomer());
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var customerDto = customer.ToCustomerDto();

        var isValidGitHubUser = await _gitHubService.IsValidGitHubUser(customer.GitHubUsername);
        if (!isValidGitHubUser)
        {
            var message = $"There is no GitHub user with username {customer.GitHubUsername}";
            throw new ValidationException(message, GenerateValidationError(nameof(customer.GitHubUsername), message));
        }

        var result = await _customerRepository.UpdateAsync(customerDto);
        if (result)
        {
            await _queueMessenger.PublishAsync(customer.ToCustomerUpdatedMessage());
        }

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _customerRepository.DeleteAsync(id);

        if (result)
        {
            await _queueMessenger.PublishAsync(new CustomerDeletedMessage
            {
                Id = id
            });
        }

        return result;
    }

    private static ValidationFailure[] GenerateValidationError(string paramName, string message)
    {
        return new[]
        {
            new ValidationFailure(paramName, message)
        };
    }
}

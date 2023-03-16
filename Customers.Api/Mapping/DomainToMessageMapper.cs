using Customers.Api.Contracts.Messages;
using Customers.Api.Domain;

namespace Customers.Api.Mapping;

public static class DomainToMessageMapper
{
    public static CustomerCreatedMessage ToCustomerCreatedMessage(this Customer customer) =>
            new()
            {
                Id = customer.Id,
                Fullname = customer.FullName,
                Email = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                GitHubUsername = customer.GitHubUsername
            };

    public static CustomerUpdatedMessage ToCustomerUpdatedMessage(this Customer customer) =>
        new()
        {
            Id = customer.Id,
            Fullname = customer.FullName,
            Email = customer.FullName,
            DateOfBirth = customer.DateOfBirth,
            GitHubUsername = customer.GitHubUsername
        };
}

﻿using Customers.Api.Contracts.Data;
using Customers.Api.Domain;

namespace Customers.Api.Mapping;

public static class DomainToDtoMapper
{
    public static CustomerDataDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDataDto
        {
            Id = customer.Id,
            Email = customer.Email,
            GitHubUsername = customer.GitHubUsername,
            FullName = customer.FullName,
            DateOfBirth = customer.DateOfBirth
        };
    }
}

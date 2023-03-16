using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Contracts.Requests;

public class UpdateCustomerRequest
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }

    [FromBody] public CreateCustomerRequest Customer { get; set; } = default!;
}

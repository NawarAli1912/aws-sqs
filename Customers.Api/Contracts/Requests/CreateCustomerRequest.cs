namespace Customers.Api.Contracts.Requests;

public class CreateCustomerRequest
{
    public string GitHubUsername { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime DateOfBirth { get; init; } = default!;
}

namespace Customers.Api.Contracts.Data;

public class CustomerDAO
{
    public Guid Id { get; init; } = default!;

    public string GitHubUsername { get; init; } = default!;

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime DateOfBirth { get; init; }
}

using Customers.Api.Contracts.Data;

namespace Customers.Api.Repositories;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(CustomerDAO customer);

    Task<CustomerDAO?> GetAsync(Guid id);

    Task<IEnumerable<CustomerDAO>> GetAllAsync();

    Task<bool> UpdateAsync(CustomerDAO customer);

    Task<bool> DeleteAsync(Guid id);
}

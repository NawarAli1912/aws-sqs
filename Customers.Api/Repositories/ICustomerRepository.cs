using Customers.Api.Contracts.Data;

namespace Customers.Api.Repositories;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(CustomerDataDto customer);

    Task<CustomerDataDto?> GetAsync(Guid id);

    Task<IEnumerable<CustomerDataDto>> GetAllAsync();

    Task<bool> UpdateAsync(CustomerDataDto customer);

    Task<bool> DeleteAsync(Guid id);
}

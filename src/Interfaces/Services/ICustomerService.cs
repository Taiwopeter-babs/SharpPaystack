using FluentResults;
using SharpPayStack.Models;

namespace SharpPayStack.Services;

public interface ICustomerService
{
    Task<Result<CustomerDto>> GetCustomerAsync(int customerId);

    Task<List<CustomerDto>> GetCustomersAsync();

    Task<Result<CustomerDto>> CreateCustomerAsync(CustomerCreateDto customer);

    Task<CustomerDto> UpdateCustomerAsync(CustomerUpdateDto customer);

    Task<Result> DeleteCustomerAsync(int customerId);
}
using FluentResults;
using SharpPayStack.Models;
using SharpPayStack.Utilities;

namespace SharpPayStack.Services;

public interface ICustomerService
{
    Task<ApiResponse<CustomerDto>> GetCustomerAsync(int customerId);

    // Task<List<CustomerDto>> GetCustomersAsync();

    Task<Result<CustomerDto>> CreateCustomerAsync(CustomerCreateDto customer);

    // Task<CustomerDto> UpdateCustomerAsync(CustomerUpdateDto customer);

    Task<Result> DeleteCustomerAsync(int customerId);
}
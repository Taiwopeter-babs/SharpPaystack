using System.Linq.Expressions;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface ICustomerRepository
{
    void CreateCustomer(Customer customer);

    Task<Customer?> GetCustomerAsync(Expression<Func<Customer, bool>> expression, bool trackChanges = false);

    Task<List<Customer>> GetCustomersAsync(bool trackChanges);

    void RemoveCustomer(Customer customer);
}
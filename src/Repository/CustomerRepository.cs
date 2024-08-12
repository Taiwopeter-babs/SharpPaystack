using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Data;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;

namespace SharpPayStack.Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(DatabaseContext context) : base(context) { }

    public void CreateCustomer(Customer customer) => Create(customer);

    public async Task<Customer?> GetCustomerAsync(Expression<Func<Customer, bool>> expression, bool trackChanges = false)
    {
        return await FindByCondition(expression, trackChanges).SingleOrDefaultAsync();
    }

    public async Task<List<Customer>> GetCustomersAsync(bool trackChanges = false)
    {
        return await FindMany(trackChanges).ToListAsync();
    }

    public void RemoveCustomer(Customer customer) => Delete(customer);
}
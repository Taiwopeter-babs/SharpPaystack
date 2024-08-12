using System.Linq.Expressions;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IBankDetailsRepository
{
    void CreateBankDetails(BankDetails bankDetails);

    Task<BankDetails?> GetBankDetailsAsync(
        Expression<Func<BankDetails, bool>> expression, bool trackChanges = false
    );

    // Task<List<Customer>> GetCustomersAsync(bool trackChanges);

    // void RemoveCustomer(Customer customer);
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Data;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;

namespace SharpPayStack.Repository;

public class BankDetailsRepository : RepositoryBase<BankDetails>, IBankDetailsRepository
{
    public BankDetailsRepository(DatabaseContext context) : base(context) { }

    public void CreateBankDetails(BankDetails bankDetails) => Create(bankDetails);

    public async Task<BankDetails?> GetBankDetailsAsync(
        Expression<Func<BankDetails, bool>> expression, bool trackChanges = false
    )
    {
        return await FindByCondition(expression, trackChanges).SingleOrDefaultAsync();
    }
}
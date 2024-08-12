using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Data;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;

namespace SharpPayStack.Repository;

public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
{
    public WalletRepository(DatabaseContext context) : base(context) { }

    public void CreateWallet(Wallet wallet) => Create(wallet);


    public async Task<Wallet?>
        GetWallet(Expression<Func<Wallet, bool>> expression, bool trackChanges = false)
    {
        return await FindByCondition(expression, trackChanges).SingleOrDefaultAsync();
    }

    public async Task<List<Wallet>> GetWallets()
    {
        return await FindMany().ToListAsync();
    }

    public void RemoveWallet(Wallet wallet) => Delete(wallet);
}
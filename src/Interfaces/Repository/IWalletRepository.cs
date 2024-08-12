using System.Linq.Expressions;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IWalletRepository
{
    void CreateWallet(Wallet wallet);

    Task<Wallet?> GetWallet(Expression<Func<Wallet, bool>> expression, bool trackChanges = false);

    Task<List<Wallet>> GetWallets();

    void RemoveWallet(Wallet wallet);
}
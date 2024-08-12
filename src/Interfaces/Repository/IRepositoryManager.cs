using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace SharpPayStack.Interfaces;

public interface IRepositoryManager
{
    ICustomerRepository Customer { get; }

    IWalletRepository Wallet { get; }

    Task SaveAsync();

    public IDbContextTransaction GetTransactionObject();
}
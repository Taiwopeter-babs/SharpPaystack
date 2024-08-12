using SharpPayStack.Data;
using SharpPayStack.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SharpPayStack.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly DatabaseContext _context;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<IWalletRepository> _walletRepository;


    public RepositoryManager(DatabaseContext context)
    {
        _context = context;

        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(context));

        _walletRepository = new Lazy<IWalletRepository>(() => new WalletRepository(context));

    }

    public ICustomerRepository Customer => _customerRepository.Value;

    public IWalletRepository Wallet => _walletRepository.Value;

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    /// <summary>
    /// Get a transaction object for atomic transactions
    /// </summary>
    /// <returns></returns>
    public IDbContextTransaction GetTransactionObject() => _context.Database.BeginTransaction();
}
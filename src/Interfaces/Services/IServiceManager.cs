using SharpPayStack.Services;

namespace SharpPayStack.Interfaces;

public interface IServiceManager
{
    IAuthService AuthService { get; }
    ICustomerService CustomerService { get; }
    IWalletService WalletService { get; }

    IBankDetailsService BankDetailsService { get; }
}
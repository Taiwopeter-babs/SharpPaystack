using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;


namespace SharpPayStack.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<ICustomerService> _customerService;
    private readonly Lazy<IWalletService> _walletService;

    private readonly Lazy<IBankDetailsService> _bankDetailsService;



    public ServiceManager(
        IRepositoryManager repository,
        IMapper mapper,
        UserManager<User> userManager,
        IValidator<RegisterUserDto> userValidator,
        IAccountService accountService
        )
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(mapper, userManager, userValidator));
        _customerService = new Lazy<ICustomerService>(() => new CustomerService(mapper, repository, accountService));
        _walletService = new Lazy<IWalletService>(() => new WalletService(mapper, repository));
        _bankDetailsService = new Lazy<IBankDetailsService>(() => new BankDetailsService(mapper, repository));
    }


    public IAuthService AuthService => _authService.Value;
    public ICustomerService CustomerService => _customerService.Value;
    public IWalletService WalletService => _walletService.Value;

    public IBankDetailsService BankDetailsService => _bankDetailsService.Value;
}
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



    public ServiceManager(
        IRepositoryManager repository,
        IMapper mapper,
        UserManager<User> userManager,
        IValidator<RegisterUserDto> userValidator
        )
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(mapper, userManager, userValidator));
        _customerService = new Lazy<ICustomerService>(() => new CustomerService(mapper, repository));
        _walletService = new Lazy<IWalletService>(() => new WalletService(mapper, repository));
    }


    public IAuthService AuthService => _authService.Value;
    public ICustomerService CustomerService => _customerService.Value;
    public IWalletService WalletService => _walletService.Value;
}
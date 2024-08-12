using FluentResults;
using SharpPayStack.Exceptions;
using SharpPayStack.Extensions;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;

namespace SharpPayStack.Services;

public class AccountService : IAccountService
{
    private readonly IPaystackService _paystackService;
    private readonly IWalletService _walletService;
    private readonly IBankDetailsService _bankDetailsService;

    public AccountService(
        IPaystackService paystackService,
        IWalletService walletService,
        IBankDetailsService bankDetailsService
    )
    {
        _paystackService = paystackService;
        _walletService = walletService;
        _bankDetailsService = bankDetailsService;
    }

    public async Task<Result> CreateCustomerAccount(Customer customer)
    {
        try
        {
            // create customer wallet
            CreateWalletDto walletToSave = customer.ToCreateWalletDto();

            var newWalletResult = await _walletService.CreateWalletAsync(walletToSave);

            // create paystack customer
            var paystackCustomerPayload = customer.ToPaystackDto();

            var paystackCustomer = await _paystackService.CreateCustomer(paystackCustomerPayload);

            if (!paystackCustomer.Status)
                throw new PaystackCustomerNotCreatedException(customer.Email!);

            // create dedicated virtual account
            var dedicatedVirtualAccount = await _paystackService
                .CreateCustomerVirtualAccount(paystackCustomer.Data.CustomerCode!);

            if (!dedicatedVirtualAccount.Status)
                throw new PaystackVirtualAccountException(paystackCustomer.Data.CustomerCode!);

            // add bank details
            var bankDetailsDto = new CreateBankDetailsDto
            {
                BankName = dedicatedVirtualAccount.Bank.Name,
                AccountName = dedicatedVirtualAccount.AccountName,
                AccountNumber = dedicatedVirtualAccount.AccountNumber,
                CustomerId = customer.Id
            };

            var newBankDetailsResult = await _bankDetailsService.CreateBankDetailsAsync(bankDetailsDto);

            customer.BankDetails = newBankDetailsResult.Value;
            customer.Wallet = newWalletResult.Value;

            return Result.Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task TransferFunds()
    {
        throw new NotImplementedException();
    }
}
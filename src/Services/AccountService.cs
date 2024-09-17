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

            var paystackCustomerResult = await _paystackService.CreateCustomer(paystackCustomerPayload);

            if (paystackCustomerResult.IsFailed)
                return Result.Fail(paystackCustomerResult.Errors);

            // create dedicated virtual account
            string customerCode = paystackCustomerResult.Value.Data.CustomerCode!;

            var dedicatedVirtualAccountResult = await _paystackService
                .CreateCustomerVirtualAccount(customerCode);

            if (dedicatedVirtualAccountResult.IsFailed)
                return Result.Fail(dedicatedVirtualAccountResult.Errors);

            // add bank details
            var bankDetailsDto = new CreateBankDetailsDto
            {
                BankName = dedicatedVirtualAccountResult.Value.Bank.Name,
                AccountName = dedicatedVirtualAccountResult.Value.AccountName,
                AccountNumber = dedicatedVirtualAccountResult.Value.AccountNumber,
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
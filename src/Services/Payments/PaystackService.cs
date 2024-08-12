using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Data;
using SharpPayStack.Utilities;
using Microsoft.Extensions.Options;
using SharpPayStack.Exceptions;

namespace SharpPayStack.Services;

public class PaystackService : IPaystackService
{
    private readonly IPaystackApi _payStackApi;
    private readonly PaystackOptions _options;

    public PaystackService(
        IPaystackApi paystackApi,
        IOptions<PaystackOptions> options
        )
    {
        _payStackApi = paystackApi;
        _options = options.Value;
    }

    private const string TransferReason = "Transfer";

    public async Task<CreateCustomerResponseDto> CreateCustomer(
        PaystackCreateCustomerDto customerCreateDto
    )
    {
        try
        {
            var paystackCustomer = await _payStackApi
                .CreateCustomer(customerCreateDto, _options.PaystackSecret!);


            if (!paystackCustomer.Status)
                throw new PaystackCustomerNotCreatedException(customerCreateDto.Email!);


            return paystackCustomer;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<VirtualAccountResponse>
        CreateCustomerVirtualAccount(string customerCode)
    {
        try
        {
            var payload = new PaystackCreateVirtualAccountDto
            {
                Customer = customerCode,
                PreferredBank = _options.PaystackBank!,
            };

            var data = await _payStackApi.CreateCustomerVirtualAccount(payload, _options.PaystackSecret!);

            if (!data.Status)
                throw new PaystackVirtualAccountException(customerCode);

            return data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PaystackTransferResponse> Transfer(decimal amount, string recipient)
    {
        try
        {
            var payload = new PaystackTransferDto
            {
                Amount = amount,
                Recipient = recipient,
                Source = _options.PaystackTransferSource!,
                Reason = TransferReason
            };

            var data = await _payStackApi.TransferFunds(payload, _options.PaystackSecret!);

            return data;
        }
        catch (Exception)
        {
            throw;
        }

    }
}
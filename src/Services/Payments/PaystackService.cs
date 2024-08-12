using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Data;
using SharpPayStack.Utilities;
using Microsoft.Extensions.Options;

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

            return paystackCustomer;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<VirtualAccountResponse>
        CreateCustomerVirtualAccount(PaystackCreateVirtualAccountDto virtualAccountCreateDto)
    {
        throw new NotImplementedException();
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
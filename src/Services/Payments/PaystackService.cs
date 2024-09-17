using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Data;
using SharpPayStack.Utilities;
using Microsoft.Extensions.Options;
using SharpPayStack.Exceptions;
using FluentResults;

namespace SharpPayStack.Services;

public class PaystackService : IPaystackService
{
    private readonly PaystackOptions _options;
    private readonly HttpClient _httpClient;



    public PaystackService(
        IOptions<PaystackOptions> options,
        HttpClient httpClient
        )
    {
        _options = options.Value;
        _httpClient = httpClient;
    }

    private const string TransferReason = "Transfer";

    /// <summary>
    /// <b>Create a paystack customer</b>
    /// </summary>
    public async Task<Result<CreateCustomerResponseDto>> CreateCustomer(
        PaystackCreateCustomerDto customerCreateDto
    )
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("customer", customerCreateDto);

            if (!response.IsSuccessStatusCode)
            {
                var paystackError = await response.Content.ReadFromJsonAsync<PaystackError>();

                return Result.Fail(PaystackErrors.ErrorMessage(string.Join(',', paystackError!.Message)));
            }

            var paystackCustomer = await response.Content.ReadFromJsonAsync<CreateCustomerResponseDto>();

            return Result.Ok(paystackCustomer!);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<VirtualAccountResponse>>
        CreateCustomerVirtualAccount(string customerCode)
    {
        try
        {
            var payload = new PaystackCreateVirtualAccountDto
            {
                Customer = customerCode,
                PreferredBank = _options.PaystackBank!,
            };

            var response = await _httpClient.PostAsJsonAsync("dedicated_account", payload);

            if (!response.IsSuccessStatusCode)
            {
                var paystackError = await response.Content.ReadFromJsonAsync<PaystackError>();

                return Result.Fail(PaystackErrors.ErrorMessage(string.Join(',', paystackError!.Message)));
            }
            var data = await response.Content.ReadFromJsonAsync<VirtualAccountResponse>();


            return Result.Ok(data!);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<PaystackTransferResponse>> Transfer(decimal amount, string recipient)
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

            var response = await _httpClient.PostAsJsonAsync("transfer", payload);

            if (!response.IsSuccessStatusCode)
            {
                var paystackError = await response.Content.ReadFromJsonAsync<PaystackError>();

                return Result.Fail(PaystackErrors.ErrorMessage(string.Join(',', paystackError!.Message)));
            }

            var data = await response.Content.ReadFromJsonAsync<PaystackTransferResponse>();

            return Result.Ok(data!);
        }
        catch (Exception)
        {
            throw;
        }

    }
}
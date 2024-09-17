using FluentResults;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IPaystackService
{
    Task<Result<CreateCustomerResponseDto>> CreateCustomer(
        PaystackCreateCustomerDto customerCreateDto
    );

    Task<Result<VirtualAccountResponse>> CreateCustomerVirtualAccount(string customerCode);

    Task<Result<PaystackTransferResponse>> Transfer(decimal amount, string recipient);
}
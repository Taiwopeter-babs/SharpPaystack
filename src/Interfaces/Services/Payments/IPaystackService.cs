using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IPaystackService
{
    Task<CreateCustomerResponseDto> CreateCustomer(
        PaystackCreateCustomerDto customerCreateDto
    );

    Task<VirtualAccountResponse> CreateCustomerVirtualAccount(
        PaystackCreateVirtualAccountDto virtualAccountCreateDto
    );

    Task<PaystackTransferResponse> Transfer(decimal amount, string recipient);
}
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IPaystackService
{
    Task<CreateCustomerResponseDto> CreateCustomer(
        PaystackCreateCustomerDto customerCreateDto
    );

    Task<VirtualAccountResponse> CreateCustomerVirtualAccount(string customerCode);

    Task<PaystackTransferResponse> Transfer(decimal amount, string recipient);
}
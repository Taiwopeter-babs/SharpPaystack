using SharpPayStack.Models;

namespace SharpPayStack.Extensions;

public static class CustomerExtension
{
    public static PaystackCreateCustomerDto ToPaystackDto(this Customer customer)
    {
        return new PaystackCreateCustomerDto
        {
            Email = customer.Email,
            Phone = customer.Phone,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
        };
    }

    public static CreateWalletDto ToCreateWalletDto(this Customer customer)
    {
        return new CreateWalletDto
        {
            CustomerId = customer.Id,
        };
    }
}
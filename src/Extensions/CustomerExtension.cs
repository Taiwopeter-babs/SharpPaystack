using SharpPayStack.Models;

namespace SharpPayStack.Extensions;

public static class CustomerExtension
{
    public static PaystackCreateCustomerDto ToPaystackDto(this CustomerCreateDto customerDto)
    {
        return new PaystackCreateCustomerDto
        {
            Email = customerDto.Email,
            Phone = customerDto.Phone,
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
        };
    }
}
namespace SharpPayStack.Models;


public record BaseCustomerDto
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Phone { get; init; }
}

public record CustomerDto : BaseCustomerDto
{
    public int Id { get; init; }

    public required string Email { get; init; }

    public required BankDetails BankDetails { get; init; }

    public required Wallet Wallet { get; init; }
}

public record CustomerCreateDto : BaseCustomerDto
{
    public string? Email { get; init; }
}


public record CustomerUpdateDto : BaseCustomerDto { }
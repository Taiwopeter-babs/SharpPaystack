namespace SharpPayStack.Models;


public record BankDetailsDto
{
    public int Id { get; init; }

    public string? BankName { get; init; }

    public string? AccountNumber { get; init; }

    public string? AccountName { get; init; }

    public int CustomerId { get; init; }
}

public record CreateBankDetailsDto
{
    public string? BankName { get; init; }

    public string? AccountNumber { get; init; }

    public string? AccountName { get; init; }

    public int CustomerId { get; init; }
}
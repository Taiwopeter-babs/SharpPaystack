namespace SharpPayStack.Models;

public record WalletDto
{
    public int Id { get; init; }

    public decimal Balance { get; init; }

    public string? Currency { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }

    public int CustomerId { get; init; }
}

public record CreateWalletDto
{
    public decimal Balance { get; init; } = 0.00M;

    public string? Currency { get; init; }

    public int CustomerId { get; init; }
}
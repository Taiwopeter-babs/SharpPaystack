using FluentResults;

namespace SharpPayStack.Utilities;


public static class CommonErrors
{
    public static Error ServerError(string message) => new(message);
}

public static class CustomerErrors
{
    public static Error CustomerNotFoundError(int customerId) =>
        new($"Customer with the id: {customerId} was not found");

    public static Error CustomerAlreadyExistsError(string customerEmail) =>
        new($"Customer with the email: {customerEmail} already exists");
}

public static class WalletErrors
{
    public static Error WalletNotFoundError(int walletId, int customerId) =>
        new($"Wallet with id {walletId} was not found for customer: {customerId}");
}
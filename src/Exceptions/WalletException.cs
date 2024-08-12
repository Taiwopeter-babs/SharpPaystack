namespace SharpPayStack.Exceptions;


public abstract class WalletException : Exception
{
    public WalletException(string message) : base(message) { }
}

public sealed class WalletNotCreatedException : WalletException
{
    public WalletNotCreatedException(int customerId) :
        base($"Wallet was not created for customer {customerId}")
    { }
}

public sealed class WalletNotRetrievedException : WalletException
{
    public WalletNotRetrievedException(int walletId, int customerId) :
        base($"Wallet with id: {walletId}, could not be retrieved for customer {customerId}")
    { }
}
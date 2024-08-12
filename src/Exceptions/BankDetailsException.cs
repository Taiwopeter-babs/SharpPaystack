namespace SharpPayStack.Exceptions;


public abstract class BankDetailsException : Exception
{
    public BankDetailsException(string message) : base(message) { }
}

public sealed class BankDetailsNotCreatedException : WalletException
{
    public BankDetailsNotCreatedException(int customerId) :
        base($"Bank data was not created for customer {customerId}")
    { }
}
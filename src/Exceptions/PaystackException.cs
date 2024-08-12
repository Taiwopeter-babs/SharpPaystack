namespace SharpPayStack.Exceptions;


public abstract class PaystackException : Exception
{
    public PaystackException(string message) : base(message) { }
}

public sealed class PaystackCustomerNotCreatedException : PaystackException
{
    public PaystackCustomerNotCreatedException(string customerEmail) :
        base($"Paystack account was not created for customer {customerEmail}")
    { }
}

public sealed class PaystackVirtualAccountException : PaystackException
{
    public PaystackVirtualAccountException(string paystackCustomerCode) :
        base(@$"Paystack virtual account could not be created for customer
            with paystack ref: {paystackCustomerCode}")
    { }
}
using FluentResults;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IAccountService
{
    Task<Result> CreateCustomerAccount(Customer customer);

    Task TransferFunds();
}
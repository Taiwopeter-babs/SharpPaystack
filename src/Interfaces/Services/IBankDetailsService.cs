using FluentResults;
using SharpPayStack.Models;

namespace SharpPayStack.Services;

public interface IBankDetailsService
{
    Task<Result<BankDetails>> CreateBankDetailsAsync(CreateBankDetailsDto bankDetailsDto);
}
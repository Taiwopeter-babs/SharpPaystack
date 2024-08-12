using FluentResults;
using SharpPayStack.Models;

namespace SharpPayStack.Interfaces;

public interface IWalletService
{
    Task<Result<WalletDto>> CreateWalletAsync(CreateWalletDto walletDto);

    Task<Result<WalletDto>> GetWalletAsync(int walletId, int customerId, bool trackChanges = false);

    Task<Result<List<WalletDto>>> GetWalletsAsync();

    Task<Result> RemoveWalletAsync(int walletId, int customerId);
}
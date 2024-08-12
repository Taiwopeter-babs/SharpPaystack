using System.Linq.Expressions;
using AutoMapper;
using FluentResults;
using SharpPayStack.Exceptions;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Utilities;

public class WalletService : IWalletService
{
    private readonly IRepositoryManager _repo;
    private readonly IMapper _mapper;

    public WalletService(IMapper mapper, IRepositoryManager repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Result<Wallet>> CreateWalletAsync(CreateWalletDto walletDto)
    {
        try
        {
            Wallet? walletExists = await CheckWalletAsync(wa => wa.CustomerId.Equals(walletDto.CustomerId));

            if (walletExists != null)
            {
                return Result.Ok(walletExists);
            }

            Wallet wallet = _mapper.Map<Wallet>(walletDto);

            await _repo.SaveAsync();

            return Result.Ok(wallet);
        }
        catch (Exception)
        {
            // log error message
            throw new WalletNotCreatedException(walletDto.CustomerId);
        }

    }

    public async Task<Result<WalletDto>> GetWalletAsync(int walletId, int customerId, bool trackChanges = false)
    {
        try
        {
            Wallet? wallet = await CheckWalletAsync(wa =>
                wa.Id.Equals(walletId) && wa.CustomerId.Equals(customerId));

            if (wallet == null)
                return Result.Fail(WalletErrors.WalletNotFoundError(walletId, customerId));

            return Result.Ok(_mapper.Map<WalletDto>(wallet));
        }
        catch (Exception)
        {
            throw new WalletNotRetrievedException(walletId, customerId);
        }
    }

    public async Task<Result<List<WalletDto>>> GetWalletsAsync()
    {
        try
        {
            List<Wallet> wallets = await _repo.Wallet.GetWallets();

            List<WalletDto> walletsDto = _mapper.Map<List<Wallet>, List<WalletDto>>(wallets);

            return Result.Ok(walletsDto);
        }
        catch (Exception)
        {
            string message = string.Format("An error occured with the retreival of wallets");

            return Result.Fail(CommonErrors.ServerError(message));
        }
    }

    public async Task<Result> RemoveWalletAsync(int walletId, int customerId)
    {
        try
        {
            Wallet? wallet = await CheckWalletAsync(wa =>
                wa.Id.Equals(walletId) && wa.CustomerId.Equals(customerId));

            if (wallet == null)
                return Result.Fail(WalletErrors.WalletNotFoundError(walletId, customerId));

            _repo.Wallet.RemoveWallet(wallet);

            await _repo.SaveAsync();

            return Result.Ok();
        }
        catch (Exception)
        {
            string message = string.Format(@"An error occured with the deletion of wallet 
                        {0} for customer id: {1}", walletId, customerId);

            return Result.Fail(CommonErrors.ServerError(message));
        }
    }


    private async Task<Wallet?>
        CheckWalletAsync(Expression<Func<Wallet, bool>> expression, bool trackChanges = false)
    {
        try
        {
            Wallet? wallet = await _repo.Wallet.GetWallet(expression, trackChanges);

            return wallet ?? null;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
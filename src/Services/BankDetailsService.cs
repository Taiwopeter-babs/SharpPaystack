using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Exceptions;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Utilities;

namespace SharpPayStack.Services;

public class BankDetailsService : IBankDetailsService
{
    private readonly IRepositoryManager _repo;
    private readonly IMapper _mapper;

    public BankDetailsService(
        IMapper mapper,
        IRepositoryManager repo
    )
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Result<BankDetails>> CreateBankDetailsAsync(CreateBankDetailsDto bankDetailsDto)
    {
        try
        {
            BankDetails? bankDetails = await _repo.BankDetails.GetBankDetailsAsync(
                bd => bd.CustomerId == bankDetailsDto.CustomerId
            );

            if (bankDetails != null)
                return bankDetails;

            BankDetails bankDetailsEntity = _mapper.Map<BankDetails>(bankDetailsDto);

            _repo.BankDetails.CreateBankDetails(bankDetails!);

            await _repo.SaveAsync();

            return Result.Ok(bankDetailsEntity);
        }
        catch (Exception)
        {
            throw new BankDetailsNotCreatedException(bankDetailsDto.CustomerId);
        }
    }
}
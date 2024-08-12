using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Exceptions;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Utilities;

namespace SharpPayStack.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepositoryManager _repo;
    private readonly IMapper _mapper;

    private readonly IAccountService _accountService;

    public CustomerService(
        IMapper mapper,
        IRepositoryManager repo,
        IAccountService accountService
    )
    {
        _mapper = mapper;
        _repo = repo;
        _accountService = accountService;
    }

    public async Task<Result<CustomerDto>> CreateCustomerAsync(CustomerCreateDto customerDto)
    {
        using var transaction = _repo.GetTransactionObject();
        try
        {
            Customer? customer = await _repo.Customer.GetCustomerAsync(cu => cu.Email == customerDto.Email);

            if (customer != null)
            {
                return Result.Fail(CustomerErrors.CustomerAlreadyExistsError(customerDto.Email!));
            }

            Customer customerEntity = _mapper.Map<Customer>(customerDto);

            _repo.Customer.CreateCustomer(customerEntity);

            await _repo.SaveAsync();

            // create savepoint
            await transaction.CreateSavepointAsync("BeforeWalletCreation");

            var result = await _accountService.CreateCustomerAccount(customerEntity);

            if (result.IsFailed)
                return Result.Fail(result.Errors);

            await _repo.SaveAsync();
            await transaction.CommitAsync();

            return Result.Ok(_mapper.Map<CustomerDto>(customerEntity));
        }
        catch (Exception ex) when (
            ex is WalletNotCreatedException || ex is PaystackCustomerNotCreatedException
            || ex is BankDetailsNotCreatedException || ex is PaystackVirtualAccountException
        )
        {
            await transaction.RollbackToSavepointAsync("BeforeWalletCreation");

            string message = $"Error occured while creating wallet for {customerDto.Email}, {ex.Message}";

            return Result.Fail(CommonErrors.ServerError(message));
        }
        catch (Exception ex)
        {
            string message = $"A error occured while creating a customer for {customerDto.Email}, {ex.Message}";

            return Result.Fail(CommonErrors.ServerError(message));
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public async Task<Result> DeleteCustomerAsync(int customerId)
    {
        try
        {
            Customer? customer = await _repo.Customer.GetCustomerAsync(cu => cu.Id == customerId);

            if (customer == null)
                return Result.Fail(CustomerErrors.CustomerNotFoundError(customerId));

            _repo.Customer.RemoveCustomer(customer);

            await _repo.SaveAsync();

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(CommonErrors.ServerError($"An error occured while deleting customer: {customerId}"));
        }
    }

    public async Task<Result<CustomerDto>> GetCustomerAsync(int customerId)
    {
        try
        {
            Customer? customer = await _repo.Customer.GetCustomerAsync(cu => cu.Id == customerId);

            return customer == null
                ? Result.Fail(CustomerErrors.CustomerNotFoundError(customerId))
                : Result.Ok(_mapper.Map<CustomerDto>(customer));
        }
        catch (Exception)
        {
            string message = $"An error occured while retrieving customer: {customerId}";
            return Result.Fail(CommonErrors.ServerError(message));
        }
    }

    public Task<List<CustomerDto>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto> UpdateCustomerAsync(CustomerUpdateDto customer)
    {
        throw new NotImplementedException();
    }
}
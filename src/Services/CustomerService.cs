using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SharpPayStack.Interfaces;
using SharpPayStack.Models;
using SharpPayStack.Utilities;

namespace SharpPayStack.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepositoryManager _repo;
    private readonly IMapper _mapper;

    public CustomerService(IMapper mapper, IRepositoryManager repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<Result<CustomerDto>> CreateCustomerAsync(CustomerCreateDto customerDto)
    {
        try
        {
            Customer? customer = await _repo.Customer.GetCustomerAsync(cu => cu.Email == customerDto.Email);

            if (customer != null)
            {
                return Result.Fail(CustomerErrors.CustomerAlreadyExistsError(customerDto.Email!));
            }

            Customer customerEntity = _mapper.Map<Customer>(customerDto);

            _repo.Customer.CreateCustomer(customerEntity);

            // begin transaction
            /// TODO Move transaction to the top of the method scope
            using var transaction = _repo.GetTransactionObject();

            // create a cross service class that depends on WalletService, CustomerService, and Paystack service
            // create wallet for customer

            // create paystack customer and virtual account

            // create bankDetails

            await _repo.SaveAsync();

            return Result.Ok(_mapper.Map<CustomerDto>(customerEntity));
        }
        catch (Exception)
        {
            return Result.Fail(CommonErrors.ServerError($"A error occured while creating {customerDto.Email}"));
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
using AutoMapper;
using SharpPayStack.Models;

namespace SharpPayStack.Shared;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // User Mapping
        CreateMap<RegisterUserDto, User>();

        // Customer Mapping
        CreateMap<Customer, CustomerDto>();

        CreateMap<CustomerCreateDto, Customer>();

        CreateMap<CustomerUpdateDto, Customer>();

        // Wallet Mapping
        CreateMap<Wallet, WalletDto>().ForSourceMember(src => src.Customer, opt => opt.DoNotValidate());

        // BankDetails Mapping 
        CreateMap<BankDetails, BankDetailsDto>().ForSourceMember(src => src.Customer, opt => opt.DoNotValidate());
        CreateMap<CreateBankDetailsDto, BankDetails>();
    }
}
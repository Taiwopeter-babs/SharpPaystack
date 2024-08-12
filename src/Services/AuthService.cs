using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using SharpPayStack.Models;

namespace SharpPayStack.Services;

public sealed class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    private readonly IValidator<RegisterUserDto> _validator;

    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        IValidator<RegisterUserDto> validator
    )
    {
        _mapper = mapper;
        _userManager = userManager;
        _validator = validator;
    }


    public async Task<IdentityResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(registerUserDto);

        if (!validationResult.IsValid)
        {
            // Add to Result errors array
        }

        var user = _mapper.Map<User>(registerUserDto);
        string userRole = nameof(registerUserDto.Role.Value);

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, userRole);

        return result;

    }
}
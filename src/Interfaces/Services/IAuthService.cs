using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<IdentityResult> RegisterUser(RegisterUserDto registerUserDto);
}
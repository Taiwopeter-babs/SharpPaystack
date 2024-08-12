using System.Globalization;
using SharpPayStack.Models;
using FluentValidation;
using SharpPayStack.Shared;

namespace SharpPayStack.Validators;

public class UserValidator : AbstractValidator<RegisterUserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty();
        RuleFor(user => user.LastName).NotEmpty();
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
        RuleFor(user => user.Password).NotEmpty();
        RuleFor(user => user.Role).NotEmpty().IsInEnum();
    }
}
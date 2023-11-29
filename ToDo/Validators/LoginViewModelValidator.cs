using FluentValidation;
using ToDo.Models.Account;

namespace ToDo.Validators
{
  public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
  {
    public LoginViewModelValidator()
    {
      RuleFor(x => x.Email)
          .NotEmpty().WithMessage("'Email' is required.")
          .EmailAddress().WithMessage("Valid 'Email' is required.");

      RuleFor(x => x.Password)
          .NotEmpty().WithMessage("'Password' is required.");
    }
  }
}
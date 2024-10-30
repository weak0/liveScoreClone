using FluentValidation;
using LiveScoreReporter.Application.Constants;

namespace LiveScoreReporter.Application.Validatiors
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(LoginAndPasswordSettings.MinPasswordLength)
                .WithMessage($"Password must be at least {LoginAndPasswordSettings.MinPasswordLength} characters long")
                .MaximumLength(LoginAndPasswordSettings.MaxPasswordLength)
                .WithMessage($"Password cannot be longer than {LoginAndPasswordSettings.MaxPasswordLength} characters");
        }
    }
}

using FluentValidation;
using LiveScoreReporter.Application.Constants;

namespace LiveScoreReporter.Application.Validatiors
{
    public class UsernameValidator : AbstractValidator<string>
    {
        public UsernameValidator()
        {
            RuleFor(username => username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(LoginAndPasswordSettings.MinUsernameLength)
                .WithMessage($"Username must be at least {LoginAndPasswordSettings.MinUsernameLength} characters long")
                .MaximumLength(LoginAndPasswordSettings.MaxUsernameLength)
                .WithMessage($"Username cannot be longer than {LoginAndPasswordSettings.MaxUsernameLength} characters")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("Username can only contain letters, numbers, and underscores");
        }
    }
}

using FluentValidation;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Validatiors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LiveScoreReporter.Application.Commands
{
    public record LoginCommand : IRequest<bool>
    {
        public LoginCommand(LoginForm input)
        {
            Username = input.Login;
            Password = input.Passwd;
        }

        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command.Username)
                .SetValidator(new UsernameValidator());

            RuleFor(command => command.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}

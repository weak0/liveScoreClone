using FluentValidation;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Validatiors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LiveScoreReporter.Application.Commands
{
    public record RegisterCommand : IRequest<bool>
    {
        public RegisterCommand(RegisterForm input)
        {
            Username = input.Login;
            Password = input.Passwd;
        }

        public string Username { get; init; }
        public string Password { get; init; }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.Username)
                .SetValidator(new UsernameValidator());

            RuleFor(command => command.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}

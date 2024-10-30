using LiveScoreReporter.Application.Commands;
using LiveScoreReporter.Application.Services.Interfaces;
using MediatR;

namespace LiveScoreReporter.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _authService.Login(command.Username, command.Password);

            return result;
        }
    }
}

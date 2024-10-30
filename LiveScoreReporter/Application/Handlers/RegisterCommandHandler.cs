using LiveScoreReporter.Application.Commands;
using LiveScoreReporter.Application.Services.Interfaces;
using MediatR;

namespace LiveScoreReporter.Application.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(command.Username, command.Password);

            return result;
        }
    }
}

using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Inputs.UserCommands
{
    public class AuthenticateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

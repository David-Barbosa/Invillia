using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Inputs.UserCommands
{
    public class RegisterUserCommand : ICommand
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

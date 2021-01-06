using Invillia.Domain.Commands.Inputs.UserCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;
using FluentValidator;

namespace Invillia.Domain.Commands.Handlers
{
    public class UserCommandHandler : Notifiable, ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(RegisterUserCommand command)
        {
            if (_userRepository.GetByUsername(command.Username) != null)
            {
                AddNotification("Username", "Este nome de usuário já está em uso!");
                return null;
            }

            var user = new User(command.Name,command.Username, command.Password, command.ConfirmPassword);

            AddNotifications(user.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _userRepository.Add(user);

            return new CommandResult(true, "Cadastro realizado com sucesso", user);
        }
    }
}

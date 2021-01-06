using FluentValidator;
using Invillia.Domain.Commands.Inputs.FriendCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.FriendHandlers
{
    public class RegisterFriendCommandHandler : Notifiable, ICommandHandler<RegisterFriendCommand>
    {
        private readonly IFriendRepository _friendRepository;

        public RegisterFriendCommandHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public ICommandResult Handle(RegisterFriendCommand command)
        {
            if (_friendRepository.GetByFriendName(command.Name) != null)
            {
                AddNotification("Name", "Este amigo já está cadastrado!");
                return null;
            }

            var friend = new Friend(command.Name, command.CellPhone, command.UserId);

            AddNotifications(friend.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _friendRepository.Add(friend);

            return new CommandResult(true, "Cadastro realizado com sucesso", friend);
        }
    }
}

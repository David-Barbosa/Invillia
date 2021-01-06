using FluentValidator;
using Invillia.Domain.Commands.Inputs.FriendCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.FriendHandlers
{
    public class UpdateFriendCommandHandler : Notifiable, ICommandHandler<RegisterFriendCommand>
    {
        private readonly IFriendRepository _friendRepository;

        public UpdateFriendCommandHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public ICommandResult Handle(RegisterFriendCommand command)
        {
            var friend = _friendRepository.GetById(command.Id);
            if (friend == null)
            {
                AddNotification("Friend", "Não existe amigo com esse identificador!");
                return null;
            }

            friend.UpdateInfo(command.Name, command.CellPhone);

            AddNotifications(friend.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _friendRepository.Update(friend);

            return new CommandResult(true, "Cadastro atualizado com sucesso", friend);
        }
    }
}

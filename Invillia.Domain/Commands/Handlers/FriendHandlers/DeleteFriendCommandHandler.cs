using FluentValidator;
using Invillia.Domain.Commands.Inputs.FriendCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;
using System.Linq;

namespace Invillia.Domain.Commands.Handlers.FriendHandlers
{
    public class DeleteFriendCommandHandler : Notifiable, ICommandHandler<DeleteFriendCommand>
    {
        private readonly IFriendRepository _friendRepository;

        public DeleteFriendCommandHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public ICommandResult Handle(DeleteFriendCommand command)
        {
            var friend = _friendRepository.GetById(command.Id);
            if (friend == null)
            {
                AddNotification("Friend", "Não existe amigo com esse identificador!");
                return null;
            }

            if(friend.Loans.Any(x => x.ReturnDate == null))
            {
                AddNotification("Friend", "Não é possível excluir esse cadastro, pois, existem empréstimos pendentes de devolução!");
                return null;
            }

            if (Invalid)
                return new CommandResult(false, "Por favor, verifique as mensagens", Notifications);

            friend.Delete();
            _friendRepository.Update(friend);

            return new CommandResult(true, "Cadastro excluído com sucesso", friend);
        }
    }
}

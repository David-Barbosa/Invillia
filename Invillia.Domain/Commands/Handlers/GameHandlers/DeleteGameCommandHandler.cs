using FluentValidator;
using Invillia.Domain.Commands.Inputs.GameCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;
using System.Linq;

namespace Invillia.Domain.Commands.Handlers.GameHandlers
{
    public class DeleteGameCommandHandler : Notifiable, ICommandHandler<DeleteGameCommand>
    {
        private readonly IGameRepository _gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public ICommandResult Handle(DeleteGameCommand command)
        {
            var game = _gameRepository.GetById(command.Id);
            if (game == null)
            {
                AddNotification("Game", "Não existe jogo com esse identificador!");
                return null;
            }

            if (game.Loans.Any(x => x.ReturnDate == null))
            {
                AddNotification("Game", "Não é possível excluir esse cadastro, pois, existem empréstimos pendentes de devolução!");
                return null;
            }

            if (Invalid)
                return new CommandResult(false, "Por favor, verifique as mensagens", Notifications);

            game.Delete();
            _gameRepository.Update(game);

            return new CommandResult(true, "Cadastro excluído com sucesso", game);
        }
    }
}

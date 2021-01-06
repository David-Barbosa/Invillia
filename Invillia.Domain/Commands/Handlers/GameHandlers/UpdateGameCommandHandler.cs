using FluentValidator;
using Invillia.Domain.Commands.Inputs.GameCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.GameHandlers
{
    public class UpdateGameCommandHandler : Notifiable, ICommandHandler<RegisterGameCommand>
    {
        private readonly IGameRepository _gameRepository;

        public UpdateGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public ICommandResult Handle(RegisterGameCommand command)
        {
            var game = _gameRepository.GetById(command.Id);
            if (game == null)
            {
                AddNotification("Game", "Não existe jogo com esse identificador!");
                return null;
            }

            game.UpdateInfo(command.Name);

            AddNotifications(game.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _gameRepository.Update(game);

            return new CommandResult(true, "Cadastro atualizado com sucesso", game);
        }
    }
}

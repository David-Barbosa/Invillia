using FluentValidator;
using Invillia.Domain.Commands.Inputs.GameCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.GameHandlers
{
    public class RegisterGameCommandHandler : Notifiable, ICommandHandler<RegisterGameCommand>
    {
        private readonly IGameRepository _gameRepository;

        public RegisterGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public ICommandResult Handle(RegisterGameCommand command)
        {
            if (_gameRepository.GetByGameName(command.Name) != null)
            {
                AddNotification("Name", "Este jogo já está cadastrado!");
                return null;
            }

            var game = new Game(command.Name, command.UserId);

            AddNotifications(game.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos abaixo", Notifications);

            _gameRepository.Add(game);

            return new CommandResult(true, "Cadastro realizado com sucesso", game);
        }
    }
}

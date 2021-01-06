using FluentValidator;
using Invillia.Domain.Commands.Inputs.LoanCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.LoanHandlers
{
    public class RegisterLoanCommandHandler : Notifiable, ICommandHandler<RegisterLoanCommand>
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILoanRepository _loanRepository;

        public RegisterLoanCommandHandler(IFriendRepository friendRepository, 
                                          IGameRepository gameRepository, 
                                          ILoanRepository loanRepository)
        {
            _friendRepository = friendRepository;
            _gameRepository = gameRepository;
            _loanRepository = loanRepository;
        }

        public ICommandResult Handle(RegisterLoanCommand command)
        {
            if (_friendRepository.GetById(command.FriendId) == null)
                AddNotification("Friend", "Não existe amigo com esse identificador!");

            var game = _gameRepository.GetById(command.GameId);
            if (game == null)
                AddNotification("Game", "Não existe jogo com esse identificador!");

            if (game != null && _loanRepository.ThereIsLoan(game.Id))
                AddNotification("Game", "Esse jogo possui empréstimo pendente!");

            if (Invalid)
                return new CommandResult(false, "Por favor, verifique os itens", Notifications);

            var loan = new Loan(command.UserId, command.GameId, command.FriendId);

            _loanRepository.Add(loan);
            game.NotAvailableLoan();
            _gameRepository.Update(game);

            return new CommandResult(true, "Empréstimo realizado com sucesso", loan);
        }
    }
}

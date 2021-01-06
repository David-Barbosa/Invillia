using FluentValidator;
using Invillia.Domain.Commands.Inputs.LoanCommands;
using Invillia.Domain.Commands.Outputs;
using Invillia.Domain.Interfaces;
using Invillia.Shared.Commands;

namespace Invillia.Domain.Commands.Handlers.LoanHandlers
{
    public class ReturnLoanCommandHandler : Notifiable, ICommandHandler<ReturnLoanCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanCommandHandler(IGameRepository gameRepository, ILoanRepository loanRepository)
        {
            _gameRepository = gameRepository;
            _loanRepository = loanRepository;
        }

        public ICommandResult Handle(ReturnLoanCommand command)
        {
            var loan = _loanRepository.GetById(command.LoanId);
            if (loan == null)
                AddNotification("Loan", "Não existe empréstimo com esse identificador!");

            if (Invalid)
                return new CommandResult(false, "Por favor, verifique os itens", Notifications);

            loan.ReturnLoan();

            _loanRepository.Update(loan);
            var game =_gameRepository.GetById(loan.GameId);

            game.AvailableLoan();
            _gameRepository.Update(game);

            return new CommandResult(true, "Devolução realizada com sucesso", loan);

        }
    }
}

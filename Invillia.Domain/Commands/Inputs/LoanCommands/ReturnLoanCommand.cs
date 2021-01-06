using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.LoanCommands
{
    public class ReturnLoanCommand : ICommand
    {
        public Guid LoanId { get; set; }
    }
}

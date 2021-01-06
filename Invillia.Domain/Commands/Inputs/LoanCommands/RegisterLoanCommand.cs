using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.LoanCommands
{
    public class RegisterLoanCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
    }
}

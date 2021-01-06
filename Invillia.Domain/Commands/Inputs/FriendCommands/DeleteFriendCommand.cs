using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.FriendCommands
{
    public class DeleteFriendCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}

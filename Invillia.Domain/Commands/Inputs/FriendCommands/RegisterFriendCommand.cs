using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.FriendCommands
{
    public class RegisterFriendCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public Guid UserId { get; set; }
    }
}

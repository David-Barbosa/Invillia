using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.GameCommands
{
    public class RegisterGameCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}

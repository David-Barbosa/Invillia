using Invillia.Shared.Commands;
using System;

namespace Invillia.Domain.Commands.Inputs.GameCommands
{
    public class DeleteGameCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}

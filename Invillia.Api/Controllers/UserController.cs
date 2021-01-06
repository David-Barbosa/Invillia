using Invillia.Domain.Commands.Handlers;
using Invillia.Domain.Commands.Inputs.UserCommands;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Invillia.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly UserCommandHandler _handler;

        public UserController(IUnitOfWork unitOfWork, UserCommandHandler handler) : base(unitOfWork)
        {
            _handler = handler;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("user")]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}

using Invillia.Api.Security;
using Invillia.Domain.Commands.Handlers.GameHandlers;
using Invillia.Domain.Commands.Inputs.GameCommands;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invillia.Api.Controllers
{
    [Route("[controller]")]
    public class GameController : BaseController
    {
        private readonly RegisterGameCommandHandler _handler;
        private readonly UpdateGameCommandHandler _updateHandler;
        private readonly DeleteGameCommandHandler _deleteHandler;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly IGameRepository _friendRepository;

        public GameController(RegisterGameCommandHandler handler,
                                AuthenticatedUser authenticatedUser,
                                UpdateGameCommandHandler updateHandler,
                                DeleteGameCommandHandler deleteHandler,
                                IGameRepository friendRepository,
                                IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _handler = handler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _authenticatedUser = authenticatedUser;
            _friendRepository = friendRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> Post([FromBody] RegisterGameCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }


        [HttpPut]
        [AllowAnonymous]
        [Route("update")]
        public async Task<IActionResult> Put([FromBody] RegisterGameCommand command)
        {
            var result = _updateHandler.Handle(command);
            return await Response(result, _handler.Notifications);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteGameCommand { Id = id };
            var result = _deleteHandler.Handle(command);
            return await Response(result, _handler.Notifications);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("games")]
        public async Task<IActionResult> GetAll()
        {
            var games = _friendRepository.GetAll();
            return await Response(games, null);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("game/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var game = _friendRepository.GetById(id);
            return await Response(game, null);
        }
    }
}

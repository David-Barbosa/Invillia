using Invillia.Api.Security;
using Invillia.Domain.Commands.Handlers.FriendHandlers;
using Invillia.Domain.Commands.Inputs.FriendCommands;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Invillia.Api.Controllers
{
    [Route("[controller]")]
    public class FriendController : BaseController
    {
        private readonly RegisterFriendCommandHandler _handler;
        private readonly UpdateFriendCommandHandler _updateHandler;
        private readonly DeleteFriendCommandHandler _deleteHandler;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly IFriendRepository _friendRepository;

        public FriendController(RegisterFriendCommandHandler handler,
                                AuthenticatedUser authenticatedUser,
                                UpdateFriendCommandHandler updateHandler,
                                DeleteFriendCommandHandler deleteHandler,
                                IFriendRepository friendRepository,
                                IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _handler = handler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _authenticatedUser = authenticatedUser;
            _friendRepository = friendRepository;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RegisterFriendCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }


        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Put([FromBody] RegisterFriendCommand command)
        {
            var result = _updateHandler.Handle(command);
            return await Response(result, _handler.Notifications);
        }

        [HttpDelete]
        [Authorize]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFriendCommand { Id = id };
            var result = _deleteHandler.Handle(command);
            return await Response(result, _handler.Notifications);
        }


        [HttpGet]
        [Authorize]
        [Route("friends")]
        public async Task<IActionResult> GetAll()
        {
            var friends = _friendRepository.GetAll();
            return await Response(friends, null);
        }

        [HttpGet]
        [Authorize]
        [Route("friend/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var friend = _friendRepository.GetById(id);
            return await Response(friend, null);
        }
    }
}

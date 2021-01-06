using FluentValidator;
using Invillia.Api.Security;
using Invillia.Domain.Commands.Inputs.UserCommands;
using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invillia.Api.Controllers
{
    public class AccountController : BaseController
    {
        private User _user;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Post([FromForm] AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });


            _user = _userRepository.GetByUsername(command.Username);
            if(_user == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var token = TokenService.GenerateToken(_user);
            var response = new
            {
                token = token,
                expires = DateTime.UtcNow.AddHours(2),
                user = new
                {
                    id = _user.Id,
                    name = _user.Name.ToString(),
                    username = _user.Username
                }
            };
            return Ok(response);
        }
    }
}

using Invillia.Api.Security;
using Invillia.Domain.Commands.Handlers.LoanHandlers;
using Invillia.Domain.Commands.Inputs.LoanCommands;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invillia.Api.Controllers
{
    public class LoanController : BaseController
    {
        private readonly RegisterLoanCommandHandler _registerHandler;
        private readonly ReturnLoanCommandHandler _returnLoanCommandHandler;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly ILoanRepository _loanRepository;

        public LoanController(RegisterLoanCommandHandler registerHandler,
                              ReturnLoanCommandHandler returnLoanCommandHandler,
                              AuthenticatedUser authenticatedUser,
                              ILoanRepository loanRepository,
                              IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _authenticatedUser = authenticatedUser;
            _registerHandler = registerHandler;
            _returnLoanCommandHandler = returnLoanCommandHandler;
            _loanRepository = loanRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> Post([FromBody] RegisterLoanCommand command)
        {
            var result = _registerHandler.Handle(command);
            return await Response(result, _registerHandler.Notifications);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("return/{id}")]
        public async Task<IActionResult> ReturnLoan(Guid id)
        {
            var command = new ReturnLoanCommand { LoanId = id };
            var result = _returnLoanCommandHandler.Handle(command);
            return await Response(result, _registerHandler.Notifications);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("friend/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var loans = _loanRepository.GetAllByFriendId(id);
            return await Response(loans, null);
        }
    }
}

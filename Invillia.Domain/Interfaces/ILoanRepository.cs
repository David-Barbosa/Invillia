using invillia.Domain.Interfaces;
using Invillia.Domain.Entities;
using System;
using System.Linq;

namespace Invillia.Domain.Interfaces
{
    public interface ILoanRepository : IRepository<Loan>
    {
        IQueryable<Loan> GetAllByFriendId(Guid friendId);
        bool ThereIsLoan(Guid gameId);
    }
}

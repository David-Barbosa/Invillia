using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Invillia.Infra.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        private readonly InvilliaDataContext _context;

        public LoanRepository(InvilliaDataContext context) : base(context) => _context = context;

        public IQueryable<Loan> GetAllByFriendId(Guid friendId)
        {
            return _context.Loan
                .AsNoTracking()
                .Where(x => x.FriendId == friendId);
        }

        public bool ThereIsLoan(Guid gameId)
        {
            return _context.Loan
                .AsNoTracking()
                .Any(x => x.GameId == gameId && x.ReturnDate == null);
        }
    }
}

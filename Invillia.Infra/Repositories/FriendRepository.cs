using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Invillia.Infra.Repositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        private readonly InvilliaDataContext _context;

        public FriendRepository(InvilliaDataContext context) : base(context) => _context = context;

        public Friend GetByFriendName(string name)
        {
            return _context.Friend
                .AsNoTracking()
                .FirstOrDefault(x => x.Exclude == false && x.Name.ToUpper() == name.ToUpper());
        }

        public override Friend GetById(Guid id)
        {
            return _context.Friend
               .AsNoTracking()
               .Include(y => y.Loans)
               .FirstOrDefault(x => x.Exclude == false && x.Id == id);
        }

        public override IQueryable<Friend> GetAll()
        {
            return base.GetAll().Where(x => x.Exclude == false);
        }
    }
}

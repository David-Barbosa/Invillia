using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Invillia.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly InvilliaDataContext _context;

        public UserRepository(InvilliaDataContext context) : base(context) => _context = context;

        public User GetByUsername(string username)
        {
           return _context.User
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Username == username);
        }
    }
}
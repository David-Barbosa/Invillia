using invillia.Domain.Interfaces;
using Invillia.Domain.Entities;

namespace Invillia.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
    }
}

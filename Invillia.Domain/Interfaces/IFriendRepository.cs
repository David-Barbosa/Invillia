using invillia.Domain.Interfaces;
using Invillia.Domain.Entities;

namespace Invillia.Domain.Interfaces
{
    public interface IFriendRepository : IRepository<Friend>
    {
        Friend GetByFriendName(string name);
    }
}

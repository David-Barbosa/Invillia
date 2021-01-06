using invillia.Domain.Interfaces;
using Invillia.Domain.Entities;

namespace Invillia.Domain.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetByGameName(string name);
    }
}

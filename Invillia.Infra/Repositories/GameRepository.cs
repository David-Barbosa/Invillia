using Invillia.Domain.Entities;
using Invillia.Domain.Interfaces;
using Invillia.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Invillia.Infra.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly InvilliaDataContext _context;

        public GameRepository(InvilliaDataContext context) : base(context) => _context = context;

        public Game GetByGameName(string name)
        {
            return _context.Game
                .AsNoTracking()
                .FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
        }

        public override Game GetById(Guid id)
        {
            return _context.Game
               .AsNoTracking()
               .FirstOrDefault(x => x.Exclude == false && x.Id == id);
        }

        public override IQueryable<Game> GetAll()
        {
            return base.GetAll().Where(x => x.Exclude == false);
        }
    }
}
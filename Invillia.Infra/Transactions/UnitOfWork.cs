using Invillia.Infra.Context;

namespace Invillia.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InvilliaDataContext _context;

        public UnitOfWork(InvilliaDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

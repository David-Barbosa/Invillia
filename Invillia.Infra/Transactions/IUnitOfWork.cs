using System;

namespace Invillia.Infra.Transactions
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}

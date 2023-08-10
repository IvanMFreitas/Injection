using System;
using System.Threading.Tasks;

namespace Injection.Data.Repositories
{
    public interface IUnitOfWork<TEntity> : IDisposable
        where TEntity : class
    {
        IRepository<TEntity> Repository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
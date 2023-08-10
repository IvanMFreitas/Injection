using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace Injection.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string procedureName, params SqlParameter[] parameters);

    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Injection.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Injection.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MainDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(MainDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate){
            return await _dbSet.Where(predicate).FirstAsync();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string procedureName, params SqlParameter[] parameters)
        {
            var sql = $"EXEC {procedureName} ";

            // Append parameters to the SQL string
            foreach (var parameter in parameters)
            {
                sql += $"{parameter.ParameterName}={parameter.ParameterName},";
            }

            // Remove trailing comma
            sql = sql.TrimEnd(',');

            var result = await _context.Database.SqlQueryRaw<TResult>(sql, parameters).ToListAsync();

            return result;
        }
    }
}
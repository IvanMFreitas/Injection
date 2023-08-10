using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Injection.Data.Persistence;

namespace Injection.Data.Repositories
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly MainDbContext _context;

        public UnitOfWork(MainDbContext context)
        {
            _context = context;
            Repository = new Repository<TEntity>(context);
        }

        public IRepository<TEntity> Repository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
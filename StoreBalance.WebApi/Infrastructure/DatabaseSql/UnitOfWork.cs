using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreBalance.WebApi.Domain.Common;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Infrastructure.DatabaseSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreBalanceContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(StoreBalanceContext context)
        {
            _context = context;
        }

        public virtual async Task BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public virtual void CommitTransactionAsync(int? timeout = null)
        {
            AplicaTimeout(timeout);
            _transaction.Commit();
        }

        public void DetectChanges()
        {
            _context.ChangeTracker.DetectChanges();
        }

        public void DisableAutoDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public void EnableAutoDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        public async Task<int> SaveChanges(int? timeout = null)
        {
            AplicaTimeout(timeout);
            return await _context.SaveChangesAsync();
        }

        private void AplicaTimeout(int? timeout = null)
        {
            if (timeout != null)
            {
                _context.Database.SetCommandTimeout(timeout);
            }
        }
    }
}

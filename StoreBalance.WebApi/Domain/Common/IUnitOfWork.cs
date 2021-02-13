using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Common
{
    public interface IUnitOfWork
    {
        void DisableAutoDetectChanges();
        void EnableAutoDetectChanges();
        void DetectChanges();
        Task BeginTransaction();
        void CommitTransactionAsync(int? timeout = null);
        void RollbackTransaction();
        Task<int> SaveChanges(int? timeout = null);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Repos
{
    public interface IWalletRepository
    {
        Task<Wallet> ById(Guid id);
        Task<Wallet> ByUserId(Guid userId);
        Task<IEnumerable<WalletRecord>> RecordsByPeriod(Guid userId, DateTime beginDate, DateTime endDate);
        Task InsertRecord(WalletRecord record);
        Task<IEnumerable<WalletRecord>> CreditsToApply();
        Task<Wallet> ByUserIdWithRecords(Guid userId, DateTime endDate);
    }
}

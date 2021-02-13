using Microsoft.EntityFrameworkCore;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Infrastructure.DatabaseSql
{
    public class WalletRepository : IWalletRepository
    {
        private readonly StoreBalanceContext _context;

        public WalletRepository(StoreBalanceContext context)
        {
            _context = context;
        }

        public async Task<Wallet> ById(Guid id)
        {
            var wallet = await _context.Set<Wallet>().SingleOrDefaultAsync(p => p.Id == id);
            return wallet ?? Wallet.Vazio;
        }

        public async Task<Wallet> ByUserId(Guid userId)
        {
            var wallet = await _context.Set<Wallet>().SingleOrDefaultAsync(p => p.UserId == userId);
            return wallet ?? Wallet.Vazio;
        }

        public async Task<Wallet> ByUserIdWithRecords(Guid userId, DateTime endDate)
        {
            var wallet = await _context.Set<Wallet>()
                .Include(r => r.WalletRecords.Where(x => x.ApplyAt <= endDate))
                .Where(p => p.UserId == userId)
                .SingleOrDefaultAsync();

            return wallet;
        }

        public async Task<IEnumerable<WalletRecord>> RecordsByPeriod(Guid userId, DateTime beginDate, DateTime endDate)
        {
            var walletRecords = await _context.Set<WalletRecord>()
                .Include(o => o.Wallet)
                .Where(r => (r.ApplyAt >= beginDate && r.ApplyAt <= endDate) && r.Wallet.UserId == userId)
                .ToListAsync();

            return walletRecords;
        }

        public async Task InsertRecord(WalletRecord record)
        {
            await _context.Set<WalletRecord>().AddAsync(record);
        }

        public async Task<IEnumerable<WalletRecord>> CreditsToApply()
        {
            var walletRecords = await _context.Set<WalletRecord>()
                .Include(o => o.Wallet)
                .Where(r => r.AppliedAt == null && r.ApplyAt < DateTime.Now)
                .ToListAsync();

            return walletRecords;
        }
    }
}

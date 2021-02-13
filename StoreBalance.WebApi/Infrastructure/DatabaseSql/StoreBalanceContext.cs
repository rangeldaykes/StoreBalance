using Microsoft.EntityFrameworkCore;

namespace StoreBalance.WebApi.Infrastructure.DatabaseSql
{
    public class StoreBalanceContext : DbContext
    {
        public StoreBalanceContext(DbContextOptions<StoreBalanceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WalletMap());
            modelBuilder.ApplyConfiguration(new WalletRecordMap());
        }
    }
}

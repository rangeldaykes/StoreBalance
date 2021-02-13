using Microsoft.EntityFrameworkCore;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Infrastructure.DatabaseSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBalance.Tests
{
    public class StoreBalanceFixture
    {
        public Wallet GetNewWalletModel()
        {
            return new Wallet(100, Guid.NewGuid());
        }

        public DbContextOptions<StoreBalanceContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<StoreBalanceContext>()
                .UseInMemoryDatabase(databaseName: $"Test_Database_{Guid.NewGuid()}")
                .Options;
        }
    }
}

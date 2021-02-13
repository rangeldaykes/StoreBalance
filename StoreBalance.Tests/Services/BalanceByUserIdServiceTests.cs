using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using StoreBalance.WebApi.Application;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Infrastructure.DatabaseSql;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StoreBalance.Tests.Services
{
    public class BalanceByUserIdServiceTests : IClassFixture<StoreBalanceFixture>
    {
        private readonly StoreBalanceFixture _fixture;

        public BalanceByUserIdServiceTests(StoreBalanceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Is possible to get balance by user id.")]
        public async Task Is_possible_to_get_balance_by_user_id()
        {
            using (var context = new StoreBalanceContext(_fixture.GetDbOptions()))
            {
                // arrange
                var wallet = _fixture.GetNewWalletModel();
                var record1 = new WalletRecord(WalletRecordType.Credit, 10.00M, wallet.Id, new DateTime(2021, 01, 02));
                wallet.InsertRecord(record1);

                var loggerMock = new Mock<ILogger<QueryBalanceService>>();
                var repo = new WalletRepository(context);

                context.Set<Wallet>().Add(wallet);
                context.SaveChanges();

                var service = new QueryBalanceService(loggerMock.Object, repo);

                // act
                var resultado = await service.BalanceByUserId(wallet.UserId);

                // assert
                resultado.Succeeded.Should().BeTrue();
                resultado.Data.Balance.Should().Be(wallet.Balance);
            }
        }

        [Fact(DisplayName = "Try to get wallet by absent userid must fail with notfound.")]
        public async Task Try_to_get_wallet_by_absent_userid_must_fail_with_notfound()
        {
            using (var context = new StoreBalanceContext(_fixture.GetDbOptions()))
            {
                // arrange
                var wallet = _fixture.GetNewWalletModel();
                var loggerMock = new Mock<ILogger<QueryBalanceService>>();
                var repo = new WalletRepository(context);

                context.Set<Wallet>().Add(wallet);
                context.SaveChanges();

                var service = new QueryBalanceService(loggerMock.Object, repo);

                // act
                var resultado = await service.BalanceByUserId(Guid.NewGuid());

                // assert
                resultado.Succeeded.Should().BeFalse();
                resultado.State.Should().Be(ResultState.NotFound);
            }
        }
    }
}

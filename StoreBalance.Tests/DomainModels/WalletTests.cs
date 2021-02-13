using FluentAssertions;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreBalance.Tests.DomainModels
{
    public class WalletTests : IClassFixture<StoreBalanceFixture>
    {
        private readonly StoreBalanceFixture _fixture;

        public WalletTests(StoreBalanceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Is possible to create a valid Wallet.")]
        public void Is_possible_to_create_a_valid_Wallet()
        {
            var wallet = _fixture.GetNewWalletModel();
            Assert.NotNull(wallet);
        }

        [Fact(DisplayName = "Is possible to create a valid Wallet.")]
        public void Is_possible_to_add_records_to_Wallet()
        {
            var wallet1 = _fixture.GetNewWalletModel();
            var record1 = new WalletRecord(WalletRecordType.Credit, 10.00M, wallet1.Id, new DateTime(2021, 01, 02));

            wallet1.InsertRecord(record1);

            Assert.Equal(1, wallet1.WalletRecords.Count);
        }

        [Fact(DisplayName = "Create Wallet invalid fail.")]
        public void Create_Wallet_invalid_fail()
        {
            Assert.Throws<ArgumentException>(() => new Wallet(-1, Guid.NewGuid()));

            Assert.Throws<ArgumentException>(() => new Wallet(10.12M, Guid.Empty));
        }

        [Fact(DisplayName = "Can debit from Wallet.")]
        public void Can_debit_from_Wallet()
        {
            var wallet = _fixture.GetNewWalletModel();
            wallet.Debit(10);
            wallet.Balance.Should().Be(90);
        }

        [Fact(DisplayName = "Debit negative values must be fail.")]
        public void Debit_negative_values_must_be_fail()
        {
            var wallet = _fixture.GetNewWalletModel();

            Assert.Throws<ArgumentException>(() => wallet.Debit(-10));
        }
    }
}

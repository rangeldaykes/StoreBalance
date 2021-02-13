using StoreBalance.WebApi.Domain.Common;
using System;
using System.Collections.Generic;

namespace StoreBalance.WebApi.Domain
{
    public class Wallet
    {
        public static readonly Wallet Vazio = new Wallet();

        #region Properties
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public decimal Balance { get; protected set; }
        public Guid UserId { get; protected set; }
        public ICollection<WalletRecord> WalletRecords { get; protected set; }
        #endregion

        protected Wallet() { }

        public Wallet(decimal balance, Guid userid)
        {
            if (balance < 0)
                throw new ArgumentException(ErrorsMessage.BalanceNegative);

            Check.NotEmpty(userid, ErrorsMessage.UserIdNotValid);

            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UserId = userid;
            Balance = balance;
            WalletRecords = new List<WalletRecord>();
        }

        public void Debit(decimal value)
        {
            if (value < 0)
                throw new ArgumentException(ErrorsMessage.ValueNegative);

            if (Balance < value)
                throw new ArgumentException(ErrorsMessage.BalanceLessThanValue);

            Balance -= value;
        }

        public void Credit(decimal value)
        {
            if (value < 0)
                throw new ArgumentException(ErrorsMessage.ValueNegative);

            Balance += value;
        }

        public void InsertRecord(WalletRecord record)
        {
            WalletRecords.Add(record);
        }
    }
}

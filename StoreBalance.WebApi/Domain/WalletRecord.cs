using StoreBalance.WebApi.Domain.Common;
using System;

namespace StoreBalance.WebApi.Domain
{
    public class WalletRecord
    {
        public static readonly WalletRecord Vazio = new WalletRecord();

        #region Properties
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ApplyAt { get; protected set; }
        public DateTime? AppliedAt { get; protected set; }
        public WalletRecordType RecordType { get; protected set; }
        public decimal Value { get; protected set; }
        public Guid WalletId { get; protected set; }
        public Wallet Wallet { get; protected set; }
        #endregion

        protected WalletRecord() { }

        public WalletRecord(WalletRecordType recordType, decimal value, Guid walletId) 
            : this ( recordType, value, walletId, DateTime.Now)
        {          
        }

        public WalletRecord(WalletRecordType recordType, decimal value, Guid walletId, DateTime applyAt)
        {
            if (value < 0)
                throw new ArgumentException(ErrorsMessage.ValueNegative);

            Check.NotEmpty(walletId, ErrorsMessage.WalletIdNotValid);

            Id = Guid.NewGuid();
            Value = value;
            ApplyAt = applyAt;
            CreatedAt = DateTime.Now;
            RecordType = recordType;
            WalletId = walletId;
        }

        public void ApplyRecord(DateTime applied)
        {
            if (applied < ApplyAt)
                throw new ArgumentException(ErrorsMessage.AppliedAtPass);

            AppliedAt = applied;
        }
    }
}

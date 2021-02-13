using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreBalance.WebApi.Domain;
using System;
using System.Collections.Generic;

namespace StoreBalance.WebApi.Infrastructure.DatabaseSql
{
    public class WalletRecordMap : IEntityTypeConfiguration<WalletRecord>
    {
        public void Configure(EntityTypeBuilder<WalletRecord> builder)
        {
            builder.ToTable("tb_wallet_record");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Value);
            builder.Property(x => x.RecordType);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.WalletId);

            builder.HasOne(x => x.Wallet)
                .WithMany(x => x.WalletRecords)
                .HasForeignKey(x => x.WalletId);

            builder.HasIndex(x => x.ApplyAt);
            builder.HasIndex(x => x.AppliedAt);
            builder.HasIndex(x => x.RecordType);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreBalance.WebApi.Domain;
using System;
using System.Collections.Generic;

namespace StoreBalance.WebApi.Infrastructure.DatabaseSql
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("tb_wallet");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Balance);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UserId);

            builder.HasIndex(x => x.UserId);
        }
    }
}

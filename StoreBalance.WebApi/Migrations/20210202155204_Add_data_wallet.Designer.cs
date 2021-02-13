﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StoreBalance.WebApi.Infrastructure.DatabaseSql;

namespace StoreBalance.WebApi.Migrations
{
    [DbContext(typeof(StoreBalanceContext))]
    [Migration("20210202155204_Add_data_wallet")]
    partial class Add_data_wallet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("StoreBalance.WebApi.Domain.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tb_wallet");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7609d17-9337-423f-b20d-170795fa629c"),
                            Balance = 1000.00m,
                            CreatedAt = new DateTime(2021, 2, 2, 12, 52, 3, 482, DateTimeKind.Local).AddTicks(5555),
                            UserId = new Guid("a12eedb1-7853-4800-b927-e48071834785")
                        });
                });

            modelBuilder.Entity("StoreBalance.WebApi.Domain.WalletRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AppliedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ApplyAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RecordType")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppliedAt");

                    b.HasIndex("ApplyAt");

                    b.HasIndex("RecordType");

                    b.HasIndex("WalletId");

                    b.ToTable("tb_wallet_record");
                });

            modelBuilder.Entity("StoreBalance.WebApi.Domain.WalletRecord", b =>
                {
                    b.HasOne("StoreBalance.WebApi.Domain.Wallet", "Wallet")
                        .WithMany("WalletRecords")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StoreBalance.WebApi.Domain.Wallet", b =>
                {
                    b.Navigation("WalletRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
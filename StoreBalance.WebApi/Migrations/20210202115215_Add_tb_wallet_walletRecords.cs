using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreBalance.WebApi.Migrations
{
    public partial class Add_tb_wallet_walletRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_wallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_wallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_wallet_record",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplyAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RecordType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_wallet_record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_wallet_record_tb_wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "tb_wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_wallet_UserId",
                table: "tb_wallet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_wallet_record_AppliedAt",
                table: "tb_wallet_record",
                column: "AppliedAt");

            migrationBuilder.CreateIndex(
                name: "IX_tb_wallet_record_ApplyAt",
                table: "tb_wallet_record",
                column: "ApplyAt");

            migrationBuilder.CreateIndex(
                name: "IX_tb_wallet_record_RecordType",
                table: "tb_wallet_record",
                column: "RecordType");

            migrationBuilder.CreateIndex(
                name: "IX_tb_wallet_record_WalletId",
                table: "tb_wallet_record",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_wallet_record");

            migrationBuilder.DropTable(
                name: "tb_wallet");
        }
    }
}

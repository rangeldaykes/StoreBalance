using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreBalance.WebApi.Migrations
{
    public partial class Add_data_wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_wallet",
                columns: new[] { "Id", "Balance", "CreatedAt", "UserId" },
                values: new object[] { new Guid("a7609d17-9337-423f-b20d-170795fa629c"), 1000.00m, new DateTime(2021, 2, 2, 12, 52, 3, 482, DateTimeKind.Local).AddTicks(5555), new Guid("a12eedb1-7853-4800-b927-e48071834785") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_wallet",
                keyColumn: "Id",
                keyValue: new Guid("a7609d17-9337-423f-b20d-170795fa629c"));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreBalance.WebApi.Migrations
{
    public partial class Add_data_walletRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_wallet_record",
                columns: new[] { "Id", "AppliedAt", "ApplyAt", "CreatedAt", "RecordType", "Value", "WalletId" },
                values: new object[,]
                {
                    { new Guid("92cd97fc-49d9-4f90-8aba-9fceb15de160"), new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 2, 12, 59, 5, 252, DateTimeKind.Local).AddTicks(3691), 0, 10.00m, new Guid("a7609d17-9337-423f-b20d-170795fa629c") },
                    { new Guid("b5015c5d-bbe6-4c2e-96c1-781054c877ac"), new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 2, 12, 59, 5, 252, DateTimeKind.Local).AddTicks(3754), 1, 11.50m, new Guid("a7609d17-9337-423f-b20d-170795fa629c") },
                    { new Guid("bfb0fee2-cedf-4a4f-aa27-262c7eb73119"), null, new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 2, 12, 59, 5, 252, DateTimeKind.Local).AddTicks(3759), 0, 10.00m, new Guid("a7609d17-9337-423f-b20d-170795fa629c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_wallet",
                keyColumn: "Id",
                keyValue: new Guid("bf57b5b4-9349-45ca-a7a8-463f85edb14c"));

            migrationBuilder.DeleteData(
                table: "tb_wallet_record",
                keyColumn: "Id",
                keyValue: new Guid("92cd97fc-49d9-4f90-8aba-9fceb15de160"));

            migrationBuilder.DeleteData(
                table: "tb_wallet_record",
                keyColumn: "Id",
                keyValue: new Guid("b5015c5d-bbe6-4c2e-96c1-781054c877ac"));

            migrationBuilder.DeleteData(
                table: "tb_wallet_record",
                keyColumn: "Id",
                keyValue: new Guid("bfb0fee2-cedf-4a4f-aa27-262c7eb73119"));
        }
    }
}

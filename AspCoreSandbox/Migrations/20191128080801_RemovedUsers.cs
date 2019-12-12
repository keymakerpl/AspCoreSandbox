using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreSandbox.Migrations
{
    public partial class RemovedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2019, 11, 28, 8, 8, 0, 416, DateTimeKind.Utc).AddTicks(6116));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2019, 11, 27, 13, 46, 9, 37, DateTimeKind.Utc).AddTicks(8174));
        }
    }
}

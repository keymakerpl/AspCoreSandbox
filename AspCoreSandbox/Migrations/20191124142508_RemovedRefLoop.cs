using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreSandbox.Migrations
{
    public partial class RemovedRefLoop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "OrderNumber" },
                values: new object[] { 2, new DateTime(2019, 11, 24, 14, 25, 8, 516, DateTimeKind.Utc).AddTicks(9073), "12344" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

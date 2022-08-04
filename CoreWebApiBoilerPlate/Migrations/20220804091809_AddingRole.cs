using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreWebApiBoilerPlate.Migrations
{
    public partial class AddingRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 8, 4, 9, 18, 9, 504, DateTimeKind.Utc).AddTicks(129));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Description", "IsActive", "ModifiedById", "ModifiedOn" },
                values: new object[] { 2, null, new DateTime(2022, 8, 4, 9, 18, 9, 504, DateTimeKind.Utc).AddTicks(133), "Normal", true, null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 8, 4, 9, 18, 9, 504, DateTimeKind.Utc).AddTicks(145));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 16, 12, 20, 39, 716, DateTimeKind.Utc).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 7, 16, 12, 20, 39, 716, DateTimeKind.Utc).AddTicks(744));
        }
    }
}

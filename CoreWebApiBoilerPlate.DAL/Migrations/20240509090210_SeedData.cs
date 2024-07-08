using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreWebApiBoilerPlate.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEN86qGQ9uJZ/0wBoAGyDwk8ZE29ideSEcEor9qZxKcR1Rq8CysDhEWdvoS19JTNaGQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "25d55ad283aa400af464c76d713c07ad");
        }
    }
}

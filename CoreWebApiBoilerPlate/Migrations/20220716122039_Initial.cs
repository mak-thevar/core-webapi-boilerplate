using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreWebApiBoilerPlate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentText = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<int>(type: "INTEGER", nullable: true),
                    TodoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    EmailId = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    TodoStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_TodoStatus_TodoStatusId",
                        column: x => x.TodoStatusId,
                        principalTable: "TodoStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todos_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todos_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Description", "IsActive", "ModifiedById", "ModifiedOn" },
                values: new object[] { 1, null, new DateTime(2022, 7, 16, 12, 20, 39, 716, DateTimeKind.Utc).AddTicks(726), "Admin", true, null, null });

            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Description", "IsDefault" },
                values: new object[] { 1, "Todo", true });

            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Description", "IsDefault" },
                values: new object[] { 2, "In Progress", true });

            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Description", "IsDefault" },
                values: new object[] { 3, "Completed", true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "EmailId", "IsActive", "ModifiedOn", "Name", "Password", "RoleId", "Username" },
                values: new object[] { 1, new DateTime(2022, 7, 16, 12, 20, 39, 716, DateTimeKind.Utc).AddTicks(744), "mak.thevar@outlook.com", true, null, "mak thevar", "25d55ad283aa400af464c76d713c07ad", 1, "mak-thevar" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ModifiedById",
                table: "Comments",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TodoId",
                table: "Comments",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedById",
                table: "Roles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifiedById",
                table: "Roles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CreatedById",
                table: "Todos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ModifiedById",
                table: "Todos",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoStatusId",
                table: "Todos",
                column: "TodoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailId",
                table: "Users",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Todos_TodoId",
                table: "Comments",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CreatedById",
                table: "Comments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_ModifiedById",
                table: "Comments",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_CreatedById",
                table: "Roles",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_ModifiedById",
                table: "Roles",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_CreatedById",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_ModifiedById",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "TodoStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

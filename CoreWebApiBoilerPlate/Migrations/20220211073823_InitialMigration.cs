using System;
using System.Linq;
using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Infrastructure.Data.MockData;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWebApiBoilerPlate.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(maxLength: 60, nullable: false),
                    FullName = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(maxLength: 60, nullable: false),
                    Password = table.Column<string>(maxLength: 60, nullable: false),
                    IsActive = table.Column<bool>(nullable: true, defaultValue: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostTitle = table.Column<string>(maxLength: 150, nullable: false),
                    PostBody = table.Column<string>(nullable: false),
                    PostImage = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedBy",
                table: "Posts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);


            SeedData(migrationBuilder);
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            var mockDataGenerator = MockDataBuilder.Build<User>();
            var mockedUsersList = mockDataGenerator.GenerateMockData();
            object[,] usersMockedData = new object[mockedUsersList.Count, 6];
            for (int i = 0; i < mockedUsersList.Count; i++)
            {
                usersMockedData[i, 0] = mockedUsersList[i].Username;
                usersMockedData[i, 1] = mockedUsersList[i].Password;
                usersMockedData[i, 2] = mockedUsersList[i].FullName;
                usersMockedData[i, 3] = mockedUsersList[i].Mobile;
                usersMockedData[i, 4] = mockedUsersList[i].Location;
                usersMockedData[i, 5] = mockedUsersList[i].Email;
            }
            migrationBuilder.InsertData("Users", new[] { "Username", "password", "Fullname", "mobile", "location", "email" }, usersMockedData);

            var mockDataGeneratorPost = MockDataBuilder.Build<Post>();
            var mockedPostList = mockDataGeneratorPost.GenerateMockData();
            object[,] postsMockedData = new object[mockedPostList.Count, 5];
            for (int i = 0; i < mockedPostList.Count; i++)
            {
                postsMockedData[i, 0] = mockedPostList[i].PostTitle;
                postsMockedData[i, 1] = mockedPostList[i].PostBody;
                postsMockedData[i, 2] = DateTime.Now.AddDays(new Random().Next(-2999,-90));
                postsMockedData[i, 3] = mockedPostList[i].CreatedBy;
                postsMockedData[i, 4] = mockedPostList[i]?.PostImage;
            }

            migrationBuilder.InsertData("Posts", new[] { "posttitle", "postbody", "createdon", "createdby", "postimage" }, postsMockedData);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

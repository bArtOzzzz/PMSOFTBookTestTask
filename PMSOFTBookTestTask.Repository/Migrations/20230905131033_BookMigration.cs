using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PMSOFTBookTestTask.Repository.Migrations
{
    /// <inheritdoc />
    public partial class BookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "AuthorName" },
                values: new object[,]
                {
                    { new Guid("d7ed2451-0f19-49dc-830e-fbe2d1c8bbcf"), "Ben Watson" },
                    { new Guid("df842420-d936-4030-acfa-25761db07a02"), "Jeffrey Richter" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[] { new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"), "IT Education" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { new Guid("585a13a7-62ff-4976-896c-5cf70dd3c6b7"), "User" },
                    { new Guid("599fea4c-46d3-491d-a9b2-d408b017ea76"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "GenreId", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("41823574-1739-4b8f-bb76-97df3d918cfa"), new Guid("d7ed2451-0f19-49dc-830e-fbe2d1c8bbcf"), new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"), "Высокопроизводительный код на платформе .NET", 2019 },
                    { new Guid("b101d34d-6e04-4e92-9b3e-bf97e34ac9ae"), new Guid("df842420-d936-4030-acfa-25761db07a02"), new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"), "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#", 2002 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RefreshToken", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("33bdcabc-3f00-4aa7-ae3b-d274eee520ef"), "admin@gmail.com", "$2a$11$2LjY4Q1k.dXMNY0pui8u4.tGVGpVAkSpnIhpWeJO.smZGIOPIlN2i", null, new Guid("599fea4c-46d3-491d-a9b2-d408b017ea76"), "Administrator" },
                    { new Guid("e983f9ea-ea3f-45ca-840e-86725e6d6963"), "user@gmail.com", "$2a$11$gWkXbP6mBx5.taFglFQfke2vpZKB43Q5vh.aiRWXJQoLWZ0a5bNuO", null, new Guid("585a13a7-62ff-4976-896c-5cf70dd3c6b7"), "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

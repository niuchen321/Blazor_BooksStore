using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksStore.Migrations
{
    public partial class DBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: false),
                    Press = table.Column<string>(maxLength: 50, nullable: false),
                    Pubtime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Introduction = table.Column<string>(maxLength: 500, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EditTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    BookCategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EditTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EditTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksStore.Migrations
{
    public partial class removeAllIntId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCategoryId",
                table: "BookCategory");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCategoryId",
                table: "BookCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

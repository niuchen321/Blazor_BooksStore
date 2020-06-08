using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksStore.Migrations
{
    public partial class removeIntId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

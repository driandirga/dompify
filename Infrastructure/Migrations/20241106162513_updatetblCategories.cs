using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DompifyAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetblCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_categoies",
                table: "categoies");

            migrationBuilder.RenameTable(
                name: "categoies",
                newName: "categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "categoies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoies",
                table: "categoies",
                column: "id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DompifyAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateNewMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phote",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "roles",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phote",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "roles");
        }
    }
}

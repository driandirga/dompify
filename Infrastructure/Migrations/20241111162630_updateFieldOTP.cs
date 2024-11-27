using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DompifyAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code",
                table: "otps",
                newName: "token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "token",
                table: "otps",
                newName: "code");
        }
    }
}

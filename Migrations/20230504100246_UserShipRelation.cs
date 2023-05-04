using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarshipsRPGAlpha.Migrations
{
    /// <inheritdoc />
    public partial class UserShipRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ships_UserId",
                table: "Ships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Users_UserId",
                table: "Ships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Users_UserId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_UserId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ships");
        }
    }
}

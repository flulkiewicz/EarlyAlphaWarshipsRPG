using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarshipsRPGAlpha.Migrations
{
    /// <inheritdoc />
    public partial class PvPStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Ships");
        }
    }
}

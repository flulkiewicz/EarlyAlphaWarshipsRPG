using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarshipsRPGAlpha.Migrations
{
    /// <inheritdoc />
    public partial class MainGunModelFixedDmgAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "MainGuns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Damage",
                table: "MainGuns");
        }
    }
}

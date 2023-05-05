using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarshipsRPGAlpha.Migrations
{
    /// <inheritdoc />
    public partial class SpecialWaeponry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialWaepons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialWaepons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipSpecialWaepon",
                columns: table => new
                {
                    ShipsId = table.Column<int>(type: "int", nullable: false),
                    SpecialWaeponsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipSpecialWaepon", x => new { x.ShipsId, x.SpecialWaeponsId });
                    table.ForeignKey(
                        name: "FK_ShipSpecialWaepon_Ships_ShipsId",
                        column: x => x.ShipsId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipSpecialWaepon_SpecialWaepons_SpecialWaeponsId",
                        column: x => x.SpecialWaeponsId,
                        principalTable: "SpecialWaepons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipSpecialWaepon_SpecialWaeponsId",
                table: "ShipSpecialWaepon",
                column: "SpecialWaeponsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipSpecialWaepon");

            migrationBuilder.DropTable(
                name: "SpecialWaepons");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarshipsRPGAlpha.Migrations
{
    /// <inheritdoc />
    public partial class SpecialWaeponDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SpecialWaepons",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 250, "Torpedo" },
                    { 2, 150, "Rocket" },
                    { 3, 300, "Depth Charge" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SpecialWaepons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpecialWaepons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpecialWaepons",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

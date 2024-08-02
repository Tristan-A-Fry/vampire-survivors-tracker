using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api.Migrations
{
    /// <inheritdoc />
    public partial class mapsWeaponsTools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Weapons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tools",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Maps",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Maps",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Forrest" });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "CDR" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Sword" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Weapons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tools",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Maps",
                newName: "name");
        }
    }
}

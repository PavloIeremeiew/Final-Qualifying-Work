using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupermarketStorageSystem.Gates.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataAndFixLogTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultingStock",
                table: "InventoryLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AuthorizedUsers",
                columns: new[] { "Id", "PasswordHash", "RoleId", "Username" },
                values: new object[,]
                {
                    { "managerID", "PasswordHashmanager123", 2, "manager" },
                    { "storekeeperID", "PasswordHashstorekeeper123", 3, "storekeeper" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorizedUsers",
                keyColumn: "Id",
                keyValue: "managerID");

            migrationBuilder.DeleteData(
                table: "AuthorizedUsers",
                keyColumn: "Id",
                keyValue: "storekeeperID");

            migrationBuilder.DropColumn(
                name: "ResultingStock",
                table: "InventoryLogs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupermarketStorageSystem.Gates.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialRolesAndAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedUser_Role_RoleId",
                table: "AuthorizedUser");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_AuthorizedUser_UserId",
                table: "InventoryLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizedUser",
                table: "AuthorizedUser");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AuthorizedUser",
                newName: "AuthorizedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizedUser_RoleId",
                table: "AuthorizedUsers",
                newName: "IX_AuthorizedUsers_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizedUsers",
                table: "AuthorizedUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Manager" },
                    { 3, "Storekeeper" }
                });

            migrationBuilder.InsertData(
                table: "AuthorizedUsers",
                columns: new[] { "Id", "PasswordHash", "RoleId", "Username" },
                values: new object[] { "adminID", "PasswordHashadmin123", 1, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedUsers_Roles_RoleId",
                table: "AuthorizedUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_AuthorizedUsers_UserId",
                table: "InventoryLogs",
                column: "UserId",
                principalTable: "AuthorizedUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizedUsers_Roles_RoleId",
                table: "AuthorizedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_AuthorizedUsers_UserId",
                table: "InventoryLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizedUsers",
                table: "AuthorizedUsers");

            migrationBuilder.DeleteData(
                table: "AuthorizedUsers",
                keyColumn: "Id",
                keyValue: "adminID");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "AuthorizedUsers",
                newName: "AuthorizedUser");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizedUsers_RoleId",
                table: "AuthorizedUser",
                newName: "IX_AuthorizedUser_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizedUser",
                table: "AuthorizedUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizedUser_Role_RoleId",
                table: "AuthorizedUser",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_AuthorizedUser_UserId",
                table: "InventoryLogs",
                column: "UserId",
                principalTable: "AuthorizedUser",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeConnectAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedUserUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserProfileId",
                table: "AspNetRoles",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_UserProfiles_UserProfileId",
                table: "AspNetRoles",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_UserProfiles_UserProfileId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserProfileId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "AspNetRoles");
        }
    }
}

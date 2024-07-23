using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDirectory.Repository.Migrations
{
    /// <inheritdoc />
    public partial class roleLocationsAndProfilePictureAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_LocationId",
                table: "Roles",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Locations_LocationId",
                table: "Roles",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Locations_LocationId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_LocationId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Employees");
        }
    }
}

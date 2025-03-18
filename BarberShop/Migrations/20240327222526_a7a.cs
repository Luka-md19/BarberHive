using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class a7a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_TeamSections_TeamSectionId1",
                table: "Barbers");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_TeamSectionId1",
                table: "Barbers");



            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd430c5-8b39-4b8c-bb01-bf63cc918244");

            migrationBuilder.DropColumn(
                name: "TeamSectionId1",
                table: "Barbers");

    
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "TeamSectionId1",
                table: "Barbers",
                type: "int",
                nullable: true);



            migrationBuilder.CreateIndex(
                name: "IX_Barbers_TeamSectionId1",
                table: "Barbers",
                column: "TeamSectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_TeamSections_TeamSectionId1",
                table: "Barbers",
                column: "TeamSectionId1",
                principalTable: "TeamSections",
                principalColumn: "Id");
        }
    }
}

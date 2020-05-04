using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class cpumanuf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "CPUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_ManufacturerId",
                table: "CPUs",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_Manufacturers_ManufacturerId",
                table: "CPUs",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_Manufacturers_ManufacturerId",
                table: "CPUs");

            migrationBuilder.DropIndex(
                name: "IX_CPUs_ManufacturerId",
                table: "CPUs");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "CPUs");
        }
    }
}

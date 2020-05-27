using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class year : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "VideoCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "SSDs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "RAMs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "PowerSupplies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "MotherBoards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HDDs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "CPUs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "SSDs");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "RAMs");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "MotherBoards");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HDDs");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "CPUs");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "ComputerAssemblies");
        }
    }
}

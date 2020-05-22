using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "VideoCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SSDs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "RAMs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PowerSupplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PCCases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MotherBoards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "HDDs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GPUs",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CPUs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "SSDs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "RAMs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PCCases");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "MotherBoards");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "HDDs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CPUs");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "GPUs",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class fixnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VideoCardInterfaces",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RequirementTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RAMTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OuterMemoryInterfaces",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OuterMemoryFormFactors",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MotherBoardNorthBridges",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MotherBoardFormFactors",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GraphicMemoryTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "VideoCardInterfaces",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "RequirementTypes",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "RAMTypes",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "OuterMemoryInterfaces",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "OuterMemoryFormFactors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "MotherBoardNorthBridges",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "MotherBoardFormFactors",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "GraphicMemoryTypes",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}

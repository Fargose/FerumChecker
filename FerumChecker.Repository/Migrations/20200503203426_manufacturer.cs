using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class manufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
           name: "Name",
           table: "ComputerAssemblies",
           nullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComputerAssemblies",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "ComputerAssemblyId",
                table: "ComputerAssemblies");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComputerAssemblies",
                table: "ComputerAssemblies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComputerAssemblies",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComputerAssemblies");

            migrationBuilder.AddColumn<int>(
                name: "ComputerAssemblyId",
                table: "ComputerAssemblies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComputerAssemblies",
                table: "ComputerAssemblies",
                column: "ComputerAssemblyId");
        }
    }
}

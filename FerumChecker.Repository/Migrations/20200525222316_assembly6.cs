using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class assembly6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies");

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "ComputerAssemblies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PCCaseId",
                table: "ComputerAssemblies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MotherBoardId",
                table: "ComputerAssemblies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CPUId",
                table: "ComputerAssemblies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies",
                column: "CPUId",
                principalTable: "CPUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies",
                column: "MotherBoardId",
                principalTable: "MotherBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies",
                column: "PCCaseId",
                principalTable: "PCCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies");

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "ComputerAssemblies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PCCaseId",
                table: "ComputerAssemblies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotherBoardId",
                table: "ComputerAssemblies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CPUId",
                table: "ComputerAssemblies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies",
                column: "CPUId",
                principalTable: "CPUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies",
                column: "MotherBoardId",
                principalTable: "MotherBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies",
                column: "PCCaseId",
                principalTable: "PCCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

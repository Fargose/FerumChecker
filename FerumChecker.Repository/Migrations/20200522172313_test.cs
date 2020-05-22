using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropColumn(
                name: "PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyI");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyI",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropColumn(
                name: "PowerSupplyI",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

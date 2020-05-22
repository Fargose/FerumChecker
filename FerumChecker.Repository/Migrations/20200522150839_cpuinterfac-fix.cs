using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class cpuinterfacfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                columns: new[] { "PowerSupplyId", "PowerSupplyCPUInterfaceId" });
        }
    }
}

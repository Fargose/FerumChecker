using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class erro1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoardSlotPowerSupplies_RAMTypes_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies");

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoardSlotPowerSupplies_PowerSupplyMotherBoardInterfaces_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies",
                column: "PowerSupplyMotherBoardInterfaceId",
                principalTable: "PowerSupplyMotherBoardInterfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoardSlotPowerSupplies_PowerSupplyMotherBoardInterfaces_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies");

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoardSlotPowerSupplies_RAMTypes_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies",
                column: "PowerSupplyMotherBoardInterfaceId",
                principalTable: "RAMTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class countrycahnge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyId",
                table: "PowerSupplyCPUInterfaces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyCPUInterfaces",
                column: "PowerSupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyCPUInterfaces",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class power : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerSupplyMotherBoardInterfaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyMotherBoardInterfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Power = table.Column<int>(nullable: false),
                    GPUInputNumber = table.Column<int>(nullable: false),
                    SATAInputNumber = table.Column<int>(nullable: false),
                    CoolerSize = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    PowerSupplyMotherBoardInterfaceId = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_PowerSupplyMotherBoardInterfaces_PowerSupplyMotherBoardInterfaceId",
                        column: x => x.PowerSupplyMotherBoardInterfaceId,
                        principalTable: "PowerSupplyMotherBoardInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyCPUInterfaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PowerSupplyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyCPUInterfaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyPoswerSupplyCPUInterface",
                columns: table => new
                {
                    PowerSupplyId = table.Column<int>(nullable: false),
                    PowerSupplyCPUInterfaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyPoswerSupplyCPUInterface", x => new { x.PowerSupplyId, x.PowerSupplyCPUInterfaceId });
                    table.ForeignKey(
                        name: "FK_PowerSupplyPoswerSupplyCPUInterface_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                        column: x => x.PowerSupplyCPUInterfaceId,
                        principalTable: "PowerSupplyCPUInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyPoswerSupplyCPUInterface_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_ManufacturerId",
                table: "PowerSupplies",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_PowerSupplyMotherBoardInterfaceId",
                table: "PowerSupplies",
                column: "PowerSupplyMotherBoardInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyCPUInterfaces_PowerSupplyId",
                table: "PowerSupplyCPUInterfaces",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyPoswerSupplyCPUInterface_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPoswerSupplyCPUInterface",
                column: "PowerSupplyCPUInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerSupplyPoswerSupplyCPUInterface");

            migrationBuilder.DropTable(
                name: "PowerSupplyCPUInterfaces");

            migrationBuilder.DropTable(
                name: "PowerSupplies");

            migrationBuilder.DropTable(
                name: "PowerSupplyMotherBoardInterfaces");
        }
    }
}

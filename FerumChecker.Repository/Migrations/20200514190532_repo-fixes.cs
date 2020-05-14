using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class repofixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterface");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterface");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoCards_GraphicMemoryType_GraphicMemoryTypeId",
                table: "VideoCards");

            migrationBuilder.DropTable(
                name: "MotherBoardSlotPowerSupplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterface",
                table: "PowerSupplyPowerSupplyCPUInterface");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GraphicMemoryType",
                table: "GraphicMemoryType");

            migrationBuilder.RenameTable(
                name: "PowerSupplyPowerSupplyCPUInterface",
                newName: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.RenameTable(
                name: "GraphicMemoryType",
                newName: "GraphicMemoryTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                newName: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                columns: new[] { "PowerSupplyId", "PowerSupplyCPUInterfaceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GraphicMemoryTypes",
                table: "GraphicMemoryTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MotherBoardPowerSupplySlots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(nullable: false),
                    PowerSupplyMotherBoardInterfaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardPowerSupplySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoardPowerSupplySlots_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoardPowerSupplySlots_PowerSupplyMotherBoardInterfaces_PowerSupplyMotherBoardInterfaceId",
                        column: x => x.PowerSupplyMotherBoardInterfaceId,
                        principalTable: "PowerSupplyMotherBoardInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardPowerSupplySlots_MotherBoardId",
                table: "MotherBoardPowerSupplySlots",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardPowerSupplySlots_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardPowerSupplySlots",
                column: "PowerSupplyMotherBoardInterfaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyCPUInterfaceId",
                principalTable: "PowerSupplyCPUInterfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCards_GraphicMemoryTypes_GraphicMemoryTypeId",
                table: "VideoCards",
                column: "GraphicMemoryTypeId",
                principalTable: "GraphicMemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoCards_GraphicMemoryTypes_GraphicMemoryTypeId",
                table: "VideoCards");

            migrationBuilder.DropTable(
                name: "MotherBoardPowerSupplySlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterfaces",
                table: "PowerSupplyPowerSupplyCPUInterfaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GraphicMemoryTypes",
                table: "GraphicMemoryTypes");

            migrationBuilder.RenameTable(
                name: "PowerSupplyPowerSupplyCPUInterfaces",
                newName: "PowerSupplyPowerSupplyCPUInterface");

            migrationBuilder.RenameTable(
                name: "GraphicMemoryTypes",
                newName: "GraphicMemoryType");

            migrationBuilder.RenameIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterface",
                newName: "IX_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplyPowerSupplyCPUInterface",
                table: "PowerSupplyPowerSupplyCPUInterface",
                columns: new[] { "PowerSupplyId", "PowerSupplyCPUInterfaceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GraphicMemoryType",
                table: "GraphicMemoryType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MotherBoardSlotPowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(type: "int", nullable: false),
                    PowerSupplyMotherBoardInterfaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardSlotPowerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoardSlotPowerSupplies_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoardSlotPowerSupplies_PowerSupplyMotherBoardInterfaces_PowerSupplyMotherBoardInterfaceId",
                        column: x => x.PowerSupplyMotherBoardInterfaceId,
                        principalTable: "PowerSupplyMotherBoardInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardSlotPowerSupplies_MotherBoardId",
                table: "MotherBoardSlotPowerSupplies",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardSlotPowerSupplies_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies",
                column: "PowerSupplyMotherBoardInterfaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterface",
                column: "PowerSupplyCPUInterfaceId",
                principalTable: "PowerSupplyCPUInterfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplies_PowerSupplyId",
                table: "PowerSupplyPowerSupplyCPUInterface",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCards_GraphicMemoryType_GraphicMemoryTypeId",
                table: "VideoCards",
                column: "GraphicMemoryTypeId",
                principalTable: "GraphicMemoryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

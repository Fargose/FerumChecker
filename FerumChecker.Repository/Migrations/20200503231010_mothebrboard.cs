using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class mothebrboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerSupplyPoswerSupplyCPUInterface");

            migrationBuilder.AddColumn<int>(
                name: "Multiplier",
                table: "VideoCardInterfaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "VideoCardInterfaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SSDs",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SSDs",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HDDs",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HDDs",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MotherBoardFormFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardFormFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoardNothernBridges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardNothernBridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplyPowerSupplyCPUInterface",
                columns: table => new
                {
                    PowerSupplyId = table.Column<int>(nullable: false),
                    PowerSupplyCPUInterfaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplyPowerSupplyCPUInterface", x => new { x.PowerSupplyId, x.PowerSupplyCPUInterfaceId });
                    table.ForeignKey(
                        name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaces_PowerSupplyCPUInterfaceId",
                        column: x => x.PowerSupplyCPUInterfaceId,
                        principalTable: "PowerSupplyCPUInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSupplyPowerSupplyCPUInterface_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    MaxMemory = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    MotherBoardFormFactorId = table.Column<int>(nullable: false),
                    MotherBoardNothernBridgeId = table.Column<int>(nullable: false),
                    CPUSocketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoards_CPUSockets_CPUSocketId",
                        column: x => x.CPUSocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoards_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoards_MotherBoardFormFactors_MotherBoardFormFactorId",
                        column: x => x.MotherBoardFormFactorId,
                        principalTable: "MotherBoardFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoards_MotherBoardNothernBridges_MotherBoardNothernBridgeId",
                        column: x => x.MotherBoardNothernBridgeId,
                        principalTable: "MotherBoardNothernBridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoardOuterMemorySlots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(nullable: false),
                    OuterMemoryInterfaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardOuterMemorySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoardOuterMemorySlots_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoardOuterMemorySlots_OuterMemoryInterfaces_OuterMemoryInterfaceId",
                        column: x => x.OuterMemoryInterfaceId,
                        principalTable: "OuterMemoryInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoardRAMSlots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(nullable: false),
                    ChannelsCount = table.Column<int>(nullable: false),
                    RAMTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardRAMSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoardRAMSlots_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoardRAMSlots_RAMTypes_RAMTypeId",
                        column: x => x.RAMTypeId,
                        principalTable: "RAMTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoardSlotPowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(nullable: false),
                    PowerSupplyMotherBoardInterfaceId = table.Column<int>(nullable: false)
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
                        name: "FK_MotherBoardSlotPowerSupplies_RAMTypes_PowerSupplyMotherBoardInterfaceId",
                        column: x => x.PowerSupplyMotherBoardInterfaceId,
                        principalTable: "RAMTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoardVideoCardSlots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(nullable: false),
                    VideoCardInterfaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardVideoCardSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBoardVideoCardSlots_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotherBoardVideoCardSlots_VideoCardInterfaces_VideoCardInterfaceId",
                        column: x => x.VideoCardInterfaceId,
                        principalTable: "VideoCardInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardOuterMemorySlots_MotherBoardId",
                table: "MotherBoardOuterMemorySlots",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardOuterMemorySlots_OuterMemoryInterfaceId",
                table: "MotherBoardOuterMemorySlots",
                column: "OuterMemoryInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardRAMSlots_MotherBoardId",
                table: "MotherBoardRAMSlots",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardRAMSlots_RAMTypeId",
                table: "MotherBoardRAMSlots",
                column: "RAMTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_CPUSocketId",
                table: "MotherBoards",
                column: "CPUSocketId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_ManufacturerId",
                table: "MotherBoards",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_MotherBoardFormFactorId",
                table: "MotherBoards",
                column: "MotherBoardFormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_MotherBoardNothernBridgeId",
                table: "MotherBoards",
                column: "MotherBoardNothernBridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardSlotPowerSupplies_MotherBoardId",
                table: "MotherBoardSlotPowerSupplies",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardSlotPowerSupplies_PowerSupplyMotherBoardInterfaceId",
                table: "MotherBoardSlotPowerSupplies",
                column: "PowerSupplyMotherBoardInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardVideoCardSlots_MotherBoardId",
                table: "MotherBoardVideoCardSlots",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoardVideoCardSlots_VideoCardInterfaceId",
                table: "MotherBoardVideoCardSlots",
                column: "VideoCardInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplyPowerSupplyCPUInterface_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPowerSupplyCPUInterface",
                column: "PowerSupplyCPUInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotherBoardOuterMemorySlots");

            migrationBuilder.DropTable(
                name: "MotherBoardRAMSlots");

            migrationBuilder.DropTable(
                name: "MotherBoardSlotPowerSupplies");

            migrationBuilder.DropTable(
                name: "MotherBoardVideoCardSlots");

            migrationBuilder.DropTable(
                name: "PowerSupplyPowerSupplyCPUInterface");

            migrationBuilder.DropTable(
                name: "MotherBoards");

            migrationBuilder.DropTable(
                name: "MotherBoardFormFactors");

            migrationBuilder.DropTable(
                name: "MotherBoardNothernBridges");

            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "VideoCardInterfaces");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "VideoCardInterfaces");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SSDs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SSDs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HDDs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "HDDs");

            migrationBuilder.CreateTable(
                name: "PowerSupplyPoswerSupplyCPUInterface",
                columns: table => new
                {
                    PowerSupplyId = table.Column<int>(type: "int", nullable: false),
                    PowerSupplyCPUInterfaceId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_PowerSupplyPoswerSupplyCPUInterface_PowerSupplyCPUInterfaceId",
                table: "PowerSupplyPoswerSupplyCPUInterface",
                column: "PowerSupplyCPUInterfaceId");
        }
    }
}

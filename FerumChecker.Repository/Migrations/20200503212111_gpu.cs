using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class gpu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphicMemoryType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicMemoryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCardInterfaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCardInterfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    MemorySize = table.Column<int>(nullable: false),
                    MemoryFrequency = table.Column<int>(nullable: false),
                    MinimumPowerConsuming = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    GPUId = table.Column<int>(nullable: false),
                    VideoCardInterfaceId = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    GraphicMemoryTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoCards_GPUs_GPUId",
                        column: x => x.GPUId,
                        principalTable: "GPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoCards_GraphicMemoryType_GraphicMemoryTypeId",
                        column: x => x.GraphicMemoryTypeId,
                        principalTable: "GraphicMemoryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoCards_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoCards_VideoCardInterfaces_VideoCardInterfaceId",
                        column: x => x.VideoCardInterfaceId,
                        principalTable: "VideoCardInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_GPUId",
                table: "VideoCards",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_GraphicMemoryTypeId",
                table: "VideoCards",
                column: "GraphicMemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_ManufacturerId",
                table: "VideoCards",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_VideoCardInterfaceId",
                table: "VideoCards",
                column: "VideoCardInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoCards");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.DropTable(
                name: "GraphicMemoryType");

            migrationBuilder.DropTable(
                name: "VideoCardInterfaces");
        }
    }
}

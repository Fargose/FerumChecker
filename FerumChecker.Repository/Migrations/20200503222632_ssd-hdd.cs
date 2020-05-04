using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class ssdhdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OuterMemoryFormFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterMemoryFormFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OuterMemoryInterfaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterMemoryInterfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HDDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemorySize = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    OuterMemoryInterfaceId = table.Column<int>(nullable: false),
                    OuterMemoryFormFactorId = table.Column<int>(nullable: false),
                    DataTransferSpeed = table.Column<int>(nullable: false),
                    BufferSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HDDs_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HDDs_OuterMemoryFormFactors_OuterMemoryFormFactorId",
                        column: x => x.OuterMemoryFormFactorId,
                        principalTable: "OuterMemoryFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HDDs_OuterMemoryInterfaces_OuterMemoryInterfaceId",
                        column: x => x.OuterMemoryInterfaceId,
                        principalTable: "OuterMemoryInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemorySize = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    OuterMemoryInterfaceId = table.Column<int>(nullable: false),
                    OuterMemoryFormFactorId = table.Column<int>(nullable: false),
                    ReadSpeed = table.Column<int>(nullable: false),
                    WriteSpeed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SSDs_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSDs_OuterMemoryFormFactors_OuterMemoryFormFactorId",
                        column: x => x.OuterMemoryFormFactorId,
                        principalTable: "OuterMemoryFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSDs_OuterMemoryInterfaces_OuterMemoryInterfaceId",
                        column: x => x.OuterMemoryInterfaceId,
                        principalTable: "OuterMemoryInterfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HDDs_ManufacturerId",
                table: "HDDs",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_HDDs_OuterMemoryFormFactorId",
                table: "HDDs",
                column: "OuterMemoryFormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_HDDs_OuterMemoryInterfaceId",
                table: "HDDs",
                column: "OuterMemoryInterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SSDs_ManufacturerId",
                table: "SSDs",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_SSDs_OuterMemoryFormFactorId",
                table: "SSDs",
                column: "OuterMemoryFormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_SSDs_OuterMemoryInterfaceId",
                table: "SSDs",
                column: "OuterMemoryInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HDDs");

            migrationBuilder.DropTable(
                name: "SSDs");

            migrationBuilder.DropTable(
                name: "OuterMemoryFormFactors");

            migrationBuilder.DropTable(
                name: "OuterMemoryInterfaces");
        }
    }
}

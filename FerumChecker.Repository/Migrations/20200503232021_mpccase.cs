using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class mpccase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PCCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCCases_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCCaseMotherBoardFormFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCCaseId = table.Column<int>(nullable: false),
                    MotherBoardFormFactorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCCaseMotherBoardFormFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCCaseMotherBoardFormFactors_MotherBoardFormFactors_MotherBoardFormFactorId",
                        column: x => x.MotherBoardFormFactorId,
                        principalTable: "MotherBoardFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCCaseMotherBoardFormFactors_PCCases_PCCaseId",
                        column: x => x.PCCaseId,
                        principalTable: "PCCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCCaseOuterMemoryFormFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCCaseId = table.Column<int>(nullable: false),
                    OuterMemoryFormFactorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCCaseOuterMemoryFormFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCCaseOuterMemoryFormFactors_OuterMemoryFormFactors_OuterMemoryFormFactorId",
                        column: x => x.OuterMemoryFormFactorId,
                        principalTable: "OuterMemoryFormFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCCaseOuterMemoryFormFactors_PCCases_PCCaseId",
                        column: x => x.PCCaseId,
                        principalTable: "PCCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PCCaseMotherBoardFormFactors_MotherBoardFormFactorId",
                table: "PCCaseMotherBoardFormFactors",
                column: "MotherBoardFormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_PCCaseMotherBoardFormFactors_PCCaseId",
                table: "PCCaseMotherBoardFormFactors",
                column: "PCCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PCCaseOuterMemoryFormFactors_OuterMemoryFormFactorId",
                table: "PCCaseOuterMemoryFormFactors",
                column: "OuterMemoryFormFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_PCCaseOuterMemoryFormFactors_PCCaseId",
                table: "PCCaseOuterMemoryFormFactors",
                column: "PCCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PCCases_ManufacturerId",
                table: "PCCases",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCCaseMotherBoardFormFactors");

            migrationBuilder.DropTable(
                name: "PCCaseOuterMemoryFormFactors");

            migrationBuilder.DropTable(
                name: "PCCases");
        }
    }
}

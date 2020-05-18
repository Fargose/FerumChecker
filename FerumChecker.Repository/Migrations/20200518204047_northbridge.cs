using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class northbridge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoards_MotherBoardNothernBridges_MotherBoardNothernBridgeId",
                table: "MotherBoards");

            migrationBuilder.DropTable(
                name: "MotherBoardNothernBridges");

            migrationBuilder.CreateTable(
                name: "MotherBoardNorthBridges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardNorthBridges", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoards_MotherBoardNorthBridges_MotherBoardNothernBridgeId",
                table: "MotherBoards",
                column: "MotherBoardNothernBridgeId",
                principalTable: "MotherBoardNorthBridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoards_MotherBoardNorthBridges_MotherBoardNothernBridgeId",
                table: "MotherBoards");

            migrationBuilder.DropTable(
                name: "MotherBoardNorthBridges");

            migrationBuilder.CreateTable(
                name: "MotherBoardNothernBridges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoardNothernBridges", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoards_MotherBoardNothernBridges_MotherBoardNothernBridgeId",
                table: "MotherBoards",
                column: "MotherBoardNothernBridgeId",
                principalTable: "MotherBoardNothernBridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

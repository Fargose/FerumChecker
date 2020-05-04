using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class cpu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CoresNumber = table.Column<int>(nullable: false),
                    CoresName = table.Column<string>(maxLength: 100, nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    MaxFrequency = table.Column<int>(nullable: false),
                    ThreadsNumber = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CPUSocketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUs_CPUSockets_CPUSocketId",
                        column: x => x.CPUSocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_CPUSocketId",
                table: "CPUs",
                column: "CPUSocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUs");
        }
    }
}

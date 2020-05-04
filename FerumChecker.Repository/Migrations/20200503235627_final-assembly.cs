using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class finalassembly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComputerAssemblies",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CPUId",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ComputerAssemblies",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MotherBoardId",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PCCaseId",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyId",
                table: "ComputerAssemblies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 1000, nullable: true),
                    OwnerId = table.Column<string>(nullable: false),
                    ComputerAssemblyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_UserProfiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssemblyHDDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerAssemblyId = table.Column<int>(nullable: false),
                    HDDId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssemblyHDDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyHDDs_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyHDDs_HDDs_HDDId",
                        column: x => x.HDDId,
                        principalTable: "HDDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssemblyRAMs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerAssemblyId = table.Column<int>(nullable: false),
                    RAMId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssemblyRAMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyRAMs_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyRAMs_RAMs_RAMId",
                        column: x => x.RAMId,
                        principalTable: "RAMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssemblyRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<short>(maxLength: 5, nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    ComputerAssemblyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssemblyRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyRates_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyRates_UserProfiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssemblySSDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerAssemblyId = table.Column<int>(nullable: false),
                    SSDId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssemblySSDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblySSDs_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblySSDs_SSDs_SSDId",
                        column: x => x.SSDId,
                        principalTable: "SSDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssemblyVideoCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerAssemblyId = table.Column<int>(nullable: false),
                    VideoCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssemblyVideoCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyVideoCards_ComputerAssemblies_ComputerAssemblyId",
                        column: x => x.ComputerAssemblyId,
                        principalTable: "ComputerAssemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerAssemblyVideoCards_VideoCards_VideoCardId",
                        column: x => x.VideoCardId,
                        principalTable: "VideoCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblies_CPUId",
                table: "ComputerAssemblies",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblies_MotherBoardId",
                table: "ComputerAssemblies",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblies_OwnerId",
                table: "ComputerAssemblies",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblies_PCCaseId",
                table: "ComputerAssemblies",
                column: "PCCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblies_PowerSupplyId",
                table: "ComputerAssemblies",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ComputerAssemblyId",
                table: "Comments",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyHDDs_ComputerAssemblyId",
                table: "ComputerAssemblyHDDs",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyHDDs_HDDId",
                table: "ComputerAssemblyHDDs",
                column: "HDDId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyRAMs_ComputerAssemblyId",
                table: "ComputerAssemblyRAMs",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyRAMs_RAMId",
                table: "ComputerAssemblyRAMs",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyRates_ComputerAssemblyId",
                table: "ComputerAssemblyRates",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyRates_OwnerId",
                table: "ComputerAssemblyRates",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblySSDs_ComputerAssemblyId",
                table: "ComputerAssemblySSDs",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblySSDs_SSDId",
                table: "ComputerAssemblySSDs",
                column: "SSDId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyVideoCards_ComputerAssemblyId",
                table: "ComputerAssemblyVideoCards",
                column: "ComputerAssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssemblyVideoCards_VideoCardId",
                table: "ComputerAssemblyVideoCards",
                column: "VideoCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies",
                column: "CPUId",
                principalTable: "CPUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies",
                column: "MotherBoardId",
                principalTable: "MotherBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_UserProfiles_OwnerId",
                table: "ComputerAssemblies",
                column: "OwnerId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies",
                column: "PCCaseId",
                principalTable: "PCCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_CPUs_CPUId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_MotherBoards_MotherBoardId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_UserProfiles_OwnerId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PCCases_PCCaseId",
                table: "ComputerAssemblies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerAssemblies_PowerSupplies_PowerSupplyId",
                table: "ComputerAssemblies");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ComputerAssemblyHDDs");

            migrationBuilder.DropTable(
                name: "ComputerAssemblyRAMs");

            migrationBuilder.DropTable(
                name: "ComputerAssemblyRates");

            migrationBuilder.DropTable(
                name: "ComputerAssemblySSDs");

            migrationBuilder.DropTable(
                name: "ComputerAssemblyVideoCards");

            migrationBuilder.DropIndex(
                name: "IX_ComputerAssemblies_CPUId",
                table: "ComputerAssemblies");

            migrationBuilder.DropIndex(
                name: "IX_ComputerAssemblies_MotherBoardId",
                table: "ComputerAssemblies");

            migrationBuilder.DropIndex(
                name: "IX_ComputerAssemblies_OwnerId",
                table: "ComputerAssemblies");

            migrationBuilder.DropIndex(
                name: "IX_ComputerAssemblies_PCCaseId",
                table: "ComputerAssemblies");

            migrationBuilder.DropIndex(
                name: "IX_ComputerAssemblies_PowerSupplyId",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "CPUId",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "MotherBoardId",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "PCCaseId",
                table: "ComputerAssemblies");

            migrationBuilder.DropColumn(
                name: "PowerSupplyId",
                table: "ComputerAssemblies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComputerAssemblies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}

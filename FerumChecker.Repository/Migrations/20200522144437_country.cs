using Microsoft.EntityFrameworkCore.Migrations;

namespace FerumChecker.Repository.Migrations
{
    public partial class country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Publishers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Publishers",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Manufacturers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Developers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Developers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Developers",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CountryId",
                table: "Publishers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_CountryId",
                table: "Developers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Countries_CountryId",
                table: "Developers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Countries_CountryId",
                table: "Publishers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Countries_CountryId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Countries_CountryId",
                table: "Publishers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_CountryId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_CountryId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Developers");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Publishers",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Developers",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}

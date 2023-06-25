using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vail_Engineering.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WasteChapter_WasteCategory_CategoryId",
                table: "WasteChapter");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Record");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "WasteChapter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WasteChapter_WasteCategory_CategoryId",
                table: "WasteChapter",
                column: "CategoryId",
                principalTable: "WasteCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WasteChapter_WasteCategory_CategoryId",
                table: "WasteChapter");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "WasteChapter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WasteChapter_WasteCategory_CategoryId",
                table: "WasteChapter",
                column: "CategoryId",
                principalTable: "WasteCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

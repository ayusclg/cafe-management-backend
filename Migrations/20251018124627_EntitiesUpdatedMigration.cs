using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_01.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Category",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedById",
                table: "Category",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Users_CreatedById",
                table: "Category",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Users_CreatedById",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatedById",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Category");
        }
    }
}

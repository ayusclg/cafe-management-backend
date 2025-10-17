using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_01.Migrations
{
    /// <inheritdoc />
    public partial class Cashierpin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CashierPin",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashierPin",
                table: "Users");
        }
    }
}

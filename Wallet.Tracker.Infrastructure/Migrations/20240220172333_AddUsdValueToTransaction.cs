using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.Tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsdValueToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UsdValue",
                table: "Erc20Transaction",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsdValue",
                table: "Erc20Transaction");
        }
    }
}

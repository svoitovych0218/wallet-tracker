using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.Tracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chain",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletData",
                columns: table => new
                {
                    Address = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    MoralisStreamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletData", x => x.Address);
                });

            migrationBuilder.CreateTable(
                name: "Erc20Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletAddress = table.Column<string>(type: "text", nullable: false),
                    TxHash = table.Column<string>(type: "text", nullable: false),
                    At = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    TransferType = table.Column<int>(type: "integer", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    TokenName = table.Column<string>(type: "text", nullable: false),
                    NativeValue = table.Column<string>(type: "text", nullable: false),
                    ContractAddress = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erc20Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Erc20Transaction_WalletData_WalletAddress",
                        column: x => x.WalletAddress,
                        principalTable: "WalletData",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletChain",
                columns: table => new
                {
                    ChainId = table.Column<string>(type: "text", nullable: false),
                    WalletAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletChain", x => new { x.WalletAddress, x.ChainId });
                    table.ForeignKey(
                        name: "FK_WalletChain_Chain_ChainId",
                        column: x => x.ChainId,
                        principalTable: "Chain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletChain_WalletData_WalletAddress",
                        column: x => x.WalletAddress,
                        principalTable: "WalletData",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Erc20TransactionChain",
                columns: table => new
                {
                    Erc20TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChainId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erc20TransactionChain", x => new { x.Erc20TransactionId, x.ChainId });
                    table.ForeignKey(
                        name: "FK_Erc20TransactionChain_Chain_ChainId",
                        column: x => x.ChainId,
                        principalTable: "Chain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erc20TransactionChain_Erc20Transaction_Erc20TransactionId",
                        column: x => x.Erc20TransactionId,
                        principalTable: "Erc20Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Erc20Transaction_WalletAddress",
                table: "Erc20Transaction",
                column: "WalletAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Erc20TransactionChain_ChainId",
                table: "Erc20TransactionChain",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_Erc20TransactionChain_Erc20TransactionId",
                table: "Erc20TransactionChain",
                column: "Erc20TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletChain_ChainId",
                table: "WalletChain",
                column: "ChainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Erc20TransactionChain");

            migrationBuilder.DropTable(
                name: "WalletChain");

            migrationBuilder.DropTable(
                name: "Erc20Transaction");

            migrationBuilder.DropTable(
                name: "Chain");

            migrationBuilder.DropTable(
                name: "WalletData");
        }
    }
}

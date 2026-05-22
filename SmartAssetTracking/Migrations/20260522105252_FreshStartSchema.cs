using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAssetTracking.Migrations
{
    /// <inheritdoc />
    public partial class FreshStartSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AssetSequence");

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRateUsd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [AssetSequence]"),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePriceUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarrantyExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    FormFactor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerAssets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MobileAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [AssetSequence]"),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePriceUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarrantyExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    DeviceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileAssets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerAssets_OfficeId",
                table: "ComputerAssets",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileAssets_OfficeId",
                table: "MobileAssets",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerAssets");

            migrationBuilder.DropTable(
                name: "MobileAssets");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropSequence(
                name: "AssetSequence");
        }
    }
}

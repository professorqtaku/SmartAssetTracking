using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAssetTracking.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingComputerAssetformfactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FormFactor",
                table: "ComputerAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormFactor",
                table: "ComputerAssets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

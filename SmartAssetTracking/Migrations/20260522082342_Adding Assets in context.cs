using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAssetTracking.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssetsincontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AssetSequence");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MobileAssets",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR [AssetSequence]",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ComputerAssets",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR [AssetSequence]",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "AssetSequence");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MobileAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR [AssetSequence]")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ComputerAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR [AssetSequence]")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}

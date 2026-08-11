using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForensicsAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMethodTypeAndDeviceTypeToSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceType",
                table: "Sources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MethodType",
                table: "Sources",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "MethodType",
                table: "Sources");
        }
    }
}

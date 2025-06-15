using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.EnvironmentalAlert.Migrations
{
    /// <inheritdoc />
    public partial class resetingTheModelsPattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RM558332");

            migrationBuilder.RenameTable(
                name: "DEVICE_CONSUMPTION",
                newName: "DEVICE_CONSUMPTION",
                newSchema: "RM558332");

            migrationBuilder.RenameTable(
                name: "DEVICE",
                newName: "DEVICE",
                newSchema: "RM558332");

            migrationBuilder.RenameTable(
                name: "CONSUMPTION_ALERT",
                newName: "CONSUMPTION_ALERT",
                newSchema: "RM558332");

            migrationBuilder.AlterColumn<string>(
                name: "LOCATION",
                schema: "RM558332",
                table: "DEVICE",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DEVICE_CONSUMPTION",
                schema: "RM558332",
                newName: "DEVICE_CONSUMPTION");

            migrationBuilder.RenameTable(
                name: "DEVICE",
                schema: "RM558332",
                newName: "DEVICE");

            migrationBuilder.RenameTable(
                name: "CONSUMPTION_ALERT",
                schema: "RM558332",
                newName: "CONSUMPTION_ALERT");

            migrationBuilder.AlterColumn<string>(
                name: "LOCATION",
                table: "DEVICE",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);
        }
    }
}

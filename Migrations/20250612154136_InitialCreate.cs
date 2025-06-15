using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.EnvironmentalAlert.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEVICE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    LOCATION = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEVICE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CONSUMPTION_ALERT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DEVICE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RECORDED_CONSUMPTION = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    EXPECTED_LIMIT = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    MESSAGE = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    ALERT_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSUMPTION_ALERT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CONSUMPTION_ALERT_DEVICE_DEVICE_ID",
                        column: x => x.DEVICE_ID,
                        principalTable: "DEVICE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DEVICE_CONSUMPTION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DEVICE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CONSUMPTION_KWH = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    EXPECTED_LIMIT_KWH = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    RECORDED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEVICE_CONSUMPTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DEVICE_CONSUMPTION_DEVICE_DEVICE_ID",
                        column: x => x.DEVICE_ID,
                        principalTable: "DEVICE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMPTION_ALERT_DEVICE_ID",
                table: "CONSUMPTION_ALERT",
                column: "DEVICE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DEVICE_CONSUMPTION_DEVICE_ID",
                table: "DEVICE_CONSUMPTION",
                column: "DEVICE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONSUMPTION_ALERT");

            migrationBuilder.DropTable(
                name: "DEVICE_CONSUMPTION");

            migrationBuilder.DropTable(
                name: "DEVICE");
        }
    }
}

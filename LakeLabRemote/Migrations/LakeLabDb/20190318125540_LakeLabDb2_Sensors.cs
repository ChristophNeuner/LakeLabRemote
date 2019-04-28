using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LakeLabRemote.Migrations.LakeLabDb
{
    public partial class LakeLabDb2_Sensors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SensorId",
                table: "Values",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Lake = table.Column<string>(type: "longtext", nullable: true),
                    Location = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    SensorType = table.Column<int>(type: "int", nullable: false),
                    TimeOfCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_SensorId",
                table: "Values",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_DeviceId",
                table: "Sensor",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Sensor_SensorId",
                table: "Values",
                column: "SensorId",
                principalTable: "Sensor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Sensor_SensorId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropIndex(
                name: "IX_Values_SensorId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Values");
        }
    }
}

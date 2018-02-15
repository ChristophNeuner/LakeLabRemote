using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LakeLabRemote.Migrations.LakeLabDb
{
    public partial class LakeLabDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserDeviceAssociation",
                columns: table => new
                {
                    ComposedKey = table.Column<string>(type: "varchar(127)", nullable: false),
                    AppUserId = table.Column<string>(type: "longtext", nullable: true),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserDeviceAssociation", x => x.ComposedKey);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Depth = table.Column<string>(type: "longtext", nullable: true),
                    Ip = table.Column<string>(type: "longtext", nullable: true),
                    Lake = table.Column<string>(type: "longtext", nullable: true),
                    Location = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    TimeOfCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<float>(type: "float", nullable: false),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SensorType = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Values_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_DeviceId",
                table: "Values",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserDeviceAssociation");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}

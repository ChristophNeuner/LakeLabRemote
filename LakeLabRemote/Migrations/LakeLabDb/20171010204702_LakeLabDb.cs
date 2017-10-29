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
                name: "Devices",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    Depth = table.Column<string>(type: "longtext", nullable: true),
                    Ip = table.Column<string>(type: "longtext", nullable: true),
                    Lake = table.Column<string>(type: "longtext", nullable: true),
                    Location = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    TimeOfCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ValuesDO",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<float>(type: "float", nullable: false),
                    DeviceGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesDO", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ValuesDO_Devices_DeviceGuid",
                        column: x => x.DeviceGuid,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValuesDO_DeviceGuid",
                table: "ValuesDO",
                column: "DeviceGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuesDO");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}

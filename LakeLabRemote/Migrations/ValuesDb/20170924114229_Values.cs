using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LakeLabRemote.Migrations.ValuesDb
{
    public partial class Values : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(127)", nullable: false),
                    Depth = table.Column<string>(type: "longtext", nullable: true),
                    Ip = table.Column<string>(type: "longtext", nullable: true),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ValuesDO",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    DeviceName = table.Column<string>(type: "varchar(127)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesDO", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ValuesDO_Device_DeviceName",
                        column: x => x.DeviceName,
                        principalTable: "Device",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValuesDO_DeviceName",
                table: "ValuesDO",
                column: "DeviceName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuesDO");

            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}

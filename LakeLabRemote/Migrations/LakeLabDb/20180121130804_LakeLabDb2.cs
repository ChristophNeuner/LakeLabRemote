using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LakeLabRemote.Migrations.LakeLabDb
{
    public partial class LakeLabDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Temperature",
                table: "ValuesDO",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "ValuesTemp",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<float>(type: "float", nullable: false),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesTemp", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ValuesTemp_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValuesTemp_DeviceId",
                table: "ValuesTemp",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuesTemp");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "ValuesDO");
        }
    }
}

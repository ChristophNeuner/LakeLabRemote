using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LakeLabRemote.Migrations.ValuesDb
{
    public partial class ValuesDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Depth",
                table: "ValuesDO",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lake",
                table: "ValuesDO",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ValuesDO",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lake",
                table: "Device",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depth",
                table: "ValuesDO");

            migrationBuilder.DropColumn(
                name: "Lake",
                table: "ValuesDO");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ValuesDO");

            migrationBuilder.DropColumn(
                name: "Lake",
                table: "Device");
        }
    }
}

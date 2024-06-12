using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace platapp.Migrations
{
    public partial class modificationPoste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfLastActivity",
                table: "Poste");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "Poste");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLastActivity",
                table: "Poste",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastActivity",
                table: "Poste",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

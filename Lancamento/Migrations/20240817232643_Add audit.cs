using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lancamento.API.Migrations
{
    /// <inheritdoc />
    public partial class Addaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lancamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Lancamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Lancamentos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Lancamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Lancamentos");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Lancamentos");
        }
    }
}

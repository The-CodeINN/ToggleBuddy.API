using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToggleBuddy.API.Migrations
{
    /// <inheritdoc />
    public partial class ToggleBuddy1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Features");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Features",
                type: "datetime2",
                nullable: true);
        }
    }
}

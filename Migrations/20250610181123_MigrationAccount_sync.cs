using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiotStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAccount_sync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastSynced",
                table: "Accounts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastSynced",
                table: "Accounts");
        }
    }
}

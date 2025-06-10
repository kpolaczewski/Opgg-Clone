using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiotStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationItems_Teamid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Item0",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item1",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item2",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item3",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item4",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item5",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item6",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item0",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item2",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item3",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item4",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item5",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Item6",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Matches");
        }
    }
}

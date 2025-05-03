using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiotStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summoners_AccountId",
                table: "Summoners");

            migrationBuilder.RenameColumn(
                name: "SummonerName",
                table: "Summoners",
                newName: "EncryptedAccountId");

            migrationBuilder.AddColumn<int>(
                name: "ProfileIconId",
                table: "Summoners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Puuid",
                table: "Summoners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "RevisionDate",
                table: "Summoners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SummonerLevel",
                table: "Summoners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Puuid",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Summoners_AccountId",
                table: "Summoners",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Summoners_Puuid",
                table: "Summoners",
                column: "Puuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Puuid",
                table: "Accounts",
                column: "Puuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summoners_AccountId",
                table: "Summoners");

            migrationBuilder.DropIndex(
                name: "IX_Summoners_Puuid",
                table: "Summoners");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Puuid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ProfileIconId",
                table: "Summoners");

            migrationBuilder.DropColumn(
                name: "Puuid",
                table: "Summoners");

            migrationBuilder.DropColumn(
                name: "RevisionDate",
                table: "Summoners");

            migrationBuilder.DropColumn(
                name: "SummonerLevel",
                table: "Summoners");

            migrationBuilder.RenameColumn(
                name: "EncryptedAccountId",
                table: "Summoners",
                newName: "SummonerName");

            migrationBuilder.AlterColumn<string>(
                name: "Puuid",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Summoners_AccountId",
                table: "Summoners",
                column: "AccountId");
        }
    }
}

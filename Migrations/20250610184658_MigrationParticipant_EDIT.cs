using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiotStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationParticipant_EDIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Matches_MatchId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_MatchId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "Participants",
                newName: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Participants",
                newName: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_MatchId",
                table: "Participants",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Matches_MatchId",
                table: "Participants",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

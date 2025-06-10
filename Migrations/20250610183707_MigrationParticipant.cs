using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiotStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChampionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Puuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiotIdGameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    ChampionLevel = table.Column<int>(type: "int", nullable: false),
                    Item0 = table.Column<int>(type: "int", nullable: false),
                    Item1 = table.Column<int>(type: "int", nullable: false),
                    Item2 = table.Column<int>(type: "int", nullable: false),
                    Item3 = table.Column<int>(type: "int", nullable: false),
                    Item4 = table.Column<int>(type: "int", nullable: false),
                    Item5 = table.Column<int>(type: "int", nullable: false),
                    Item6 = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_MatchId",
                table: "Participants",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}

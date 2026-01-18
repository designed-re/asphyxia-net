using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _260117matchmakerdbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sv_matchmakers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    version = table.Column<int>(type: "int(11)", nullable: false),
                    timestamp = table.Column<long>(type: "bigint(20)", nullable: false),
                    c_version = table.Column<int>(type: "int(11)", nullable: false),
                    player_num = table.Column<int>(type: "int(11)", nullable: false),
                    player_remaining = table.Column<int>(type: "int(11)", nullable: false),
                    filter = table.Column<int>(type: "int(11)", nullable: false),
                    music_id = table.Column<int>(type: "int(11)", nullable: false),
                    seconds = table.Column<int>(type: "int(11)", nullable: false),
                    port = table.Column<int>(type: "int(11)", nullable: false),
                    global_ip = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    local_ip = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    claim = table.Column<int>(type: "int(11)", nullable: false),
                    entry_id = table.Column<int>(type: "int(11)", nullable: false),
                    SvProfileId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_sv_matchmakers_sv_profile_SvProfileId",
                        column: x => x.SvProfileId,
                        principalTable: "sv_profile",
                        principalColumn: "id");
                },
                comment: "Data store(Matchmaker) for Sound Voltex global matching")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "idx_matchmaker_search",
                table: "sv_matchmakers",
                columns: new[] { "version", "c_version", "filter", "claim", "entry_id" });

            migrationBuilder.CreateIndex(
                name: "idx_version_timestamp",
                table: "sv_matchmakers",
                columns: new[] { "version", "timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_sv_matchmakers_SvProfileId",
                table: "sv_matchmakers",
                column: "SvProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sv_matchmakers");
        }
    }
}
